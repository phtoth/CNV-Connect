using System.IO.Ports;
using System.IO;
using System.Text.Json;
using FSUIPC;

namespace CNV_Connect
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        bool SerialState = false;
        bool SIMState = false;
        string[] ManufacturerList = [];
        List<string> AircraftList = new List<string>();
        List<string> ModuleList = new List<string>();

        string SelManufacturer = "";
        string SelAircraft = "";
        string SelSoftware = "";

        List<HWModules> HardwareList = new List<HWModules>();

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadComPorts();
            btnConnectSerial.Enabled = false;
            //btnConnectSIM.Enabled = false;
            LoadAircrafts();
            InitMSFSServices();
        }

        // Carrega as aeronaves disponíveis no diretório Boards
        private void LoadAircrafts()
        {
            string[] ManufacturerListTemp = System.IO.Directory.GetDirectories("../../../Boards/");
            int ManufacturerListSize = ManufacturerListTemp.Length;
            string[] AircraftListTemp = [];

            if (ManufacturerListSize > 0)
            {
                foreach (string Item in ManufacturerListTemp)
                {
                    int Position = Item.LastIndexOf("/");
                    string ManufacturerName = Item.Substring(Position + 1);

                    ManufacturerList.Append(ManufacturerName);
                    comboAircraftManufacturer.Items.Add(ManufacturerName);
                }

                int ListControl = 0;

                AircraftList.Add(ListControl.ToString());

                foreach (string Manufacturer in ManufacturerListTemp)
                {
                    AircraftListTemp = System.IO.Directory.GetDirectories(Manufacturer);

                    foreach (string Aircraft in AircraftListTemp)
                    {
                        int Position = Aircraft.LastIndexOf("\\");
                        string AircraftName = Aircraft.Substring(Position + 1);
                        AircraftList.Add(AircraftName);
                    }

                    ListControl = ListControl + 1;
                    AircraftList.Add(ListControl.ToString());
                }
            }
            else
            {
                // Se não encontrar nenhuma aeronave, notifica o usuário e encerra a aplicação
                // If no aircraft is found, notify the user and close the application
                string message = "Nenhuma Aeronave Encontrada. A Aplicação será encerrada.";
                string title = "Erro ao carregar as aeronaves";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        // Adiciona as portas COM encontradas ao ComboBox
        // Adds the found COM ports to the ComboBox
        private void LoadComPorts()
        {
            try
            {
                foreach (string port in SerialComm.ports)
                {
                    comboConnSerial.Items.Add(port);
                }
            }
            catch
            {
                // Em caso de não conseguir carregar nenhuma porta serial, notifica o usuário e encerra a aplicação
                // If it fails to load any serial port, notify the user and close the application

                string message = "Nenhuma porta Serial Encontrada. A Aplicação será encerrada.";
                string title = "Erro ao carregar as portas seriais";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                this.Close();
            }

            // Seleciona o primeiro valor como padrão
            // Select the first value as default

            comboConnSerial.SelectedIndex = 0;
        }

        private void btnConnTest_Click(object sender, EventArgs e)
        {
            // Muda o cursor do mouse para o modo de espera
            Cursor.Current = Cursors.WaitCursor;

            // Limpa os dados da classe e textbox com a versão da placa, caso o teste já tenha sido executado anteriormente
            SerialComm.Board_Version = "";
            lblBoardVersion.Text = "";

            // Desabilita os botões de conexão com o Simulador e Placa
            btnConnectSerial.Enabled = false;
            btnConnectSIM.Enabled = false;

            // Chama o teste passando como parâmetro a porta serial
            SerialComm.TestConnection(comboConnSerial.Text);

            // Volta o cursor para o padrão
            Cursor.Current = Cursors.Default;

            // Se o teste conseguir se comunicar com a placa, traz os dados coletados via serial e popula o textbox
            // Caso contrário, exibe uma mensagem de aviso
            if (SerialComm.Board_Version != "")
            {
                lblBoardVersion.Text = SerialComm.Board_Version;
                btnConnectSerial.Enabled = true;
            }
            else
            {
                string message = "Nenhuma Placa Encontrada.";
                string title = "Erro";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, title, buttons, MessageBoxIcon.Asterisk);
            }


        }



        private void btnConnectSerial_Click(object sender, EventArgs e)
        {
            //tmrStillAlive.Enabled = true;
            if (SerialState == false)
            {
                btnConnectSerial.Enabled = false;
                btnConnectSerial.Text = "Desconectar";
                SerialComm.SerialConnet(comboConnSerial.Text);
                SerialComm.StartSerialReceiveThread();
                SerialState = false;
                btnConnectSIM.Enabled = true;
            }
            else if (SerialState == true)
            {
                btnConnectSerial.Enabled = true;
                btnConnectSerial.Text = "Conectar";
                SerialComm.StopSerialReceiveThread();
                SerialState = true;
            }
        }

        private void tmrStayinAlive_Tick(object sender, EventArgs e)
        {

        }

        private void InitMSFSServices()
        {
            // Handle events
            //MSFSVariableServices.OnLogEntryReceived += VS_OnLogEntryReceived; // Fired when the WASM module sends a log entry
            //MSFSVariableServices.OnVariableListChanged += VS_VariableListChanged; // Fired when the list of available variables is changed
            //MSFSVariableServices.OnValuesChanged += VS_OnValuesChanged; // Fired when any LVAR value changes
            // Initialise and start

            MSFSVariableServices.Init(); // Initialise 
            MSFSVariableServices.LogLevel = LOGLEVEL.LOG_LEVEL_INFO; // Set the level of logging

            MSFSVariableServices.Start();

            List<string> lvarNames = new List<string>(MSFSVariableServices.LVars.Names);
            lvarNames.Sort();
        }

        private void btnConnectSIM_Click(object sender, EventArgs e)
        {
            //DataQueue.Turret.Start();
            //Thread.Sleep(100);
            //SerialComm.SerialSend("ARE_YOU_STILL_THERE");

            // desabilita os botões

            btnConnectSIM.Enabled = false;

            comboAircraftManufacturer.Enabled = false;
            comboAircraftModel.Enabled = false; 
            comboAircraftSoft.Enabled = false;

            comboConnSerial.Enabled = false;

            btnConnTest.Enabled = false;
            btnConnectSerial.Enabled = false;


            // Conexão com o Simulador



            // Leitura dos módulos de hardware
            string ModulePath = "../../../Boards/" + SelManufacturer + "/" + SelAircraft + "/" + SelSoftware + "/";
            string[] ModulesList = System.IO.Directory.GetFiles(ModulePath);

            foreach (string Module in ModulesList)
            {
                string jsonContent = File.ReadAllText(Module);
                HWModules NewModule = JsonSerializer.Deserialize<HWModules>(jsonContent)!;
   
                if (NewModule.AircraftManufacturer == SelManufacturer && NewModule.AircraftModel == SelAircraft && NewModule.AircraftVariant == SelSoftware)
                {
                    HardwareList.Append(NewModule);
                }
                else
                {
                    MessageBox.Show("Erro ao carregar módulo de Hardware. Arquivo com erro de configuração.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    NewModule = null;
                }

            }

        }

        private void comboAircraftModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboAircraftManufacturer.Items.Count > 0)
            {
                comboAircraftSoft.Items.Clear();

                string[] SoftwareList = System.IO.Directory.GetDirectories("../../../Boards/" + comboAircraftManufacturer.SelectedItem + "/" + comboAircraftModel.SelectedItem);

                foreach (string Soft in SoftwareList)
                {
                    int Position = Soft.LastIndexOf("\\");
                    string AircraftSoft = Soft.Substring(Position + 1);
                    comboAircraftSoft.Items.Add(AircraftSoft);
                }
            }
            else
            {
                SelManufacturer = "";
                SelAircraft = "";
                SelSoftware = "";
            }

            ClearModules();
        }

        // Verifica se o módulo de simulação está instalado
        private void CheckModules()
        {
            // Lista os módulos instalados no diretório Modules
            string ModulePath = "../../../Boards/" + SelManufacturer + "/" + SelAircraft + "/" + SelSoftware + "/";
            string[] ModulesListTemp = System.IO.Directory.GetFiles(ModulePath);

            ModuleList.Clear();
            if (ModulesListTemp.Length > 0)
            {
                foreach (string Module in ModulesListTemp)
                {
                    int Position = Module.LastIndexOf("/");
                    string ModuleName = Module.Substring(Position + 1);
                    ModuleList.Add(ModuleName);
                }

                foreach (string FileName in ModuleList)
                {
                    string DataCheck = System.IO.File.ReadAllText(ModulePath + FileName);

                    using JsonDocument ModuleContent = JsonDocument.Parse(DataCheck);
                    JsonElement root = ModuleContent.RootElement;

                    if (root.TryGetProperty("BoardType", out JsonElement boardTypeElement))
                    {
                        string boardType = boardTypeElement.GetString();

                        switch (boardType)
                        {
                            case "Overhead":
                                cbOverhead.Checked = true;
                                break;
                            case "Radio":
                                cbRadio.Checked = true;
                                break;
                        }
                    }
                }
            }
        }

        private void comboAircraftManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Limpa o ComboBox de Modelos de Aeronaves e Fabricantes
            comboAircraftModel.Items.Clear();
            comboAircraftSoft.Items.Clear();

            int ManufacturerIndex = comboAircraftManufacturer.SelectedIndex;
            int[] List = [];

            int StartPosition = AircraftList.IndexOf(ManufacturerIndex.ToString());
            int EndPosition = AircraftList.IndexOf((ManufacturerIndex + 1).ToString());

            for (int i = StartPosition + 1; i < EndPosition; i++)
            {
                comboAircraftModel.Items.Add(AircraftList[i]);
            }

            ClearModules();
        }

        private void ClearModules()
        {
            cbOverhead.Checked = false;
            cbRadio.Checked = false;
        }

        private void comboAircraftSoft_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboAircraftSoft.Items.Count > 0)
            {
                SelManufacturer = comboAircraftManufacturer.SelectedItem.ToString();
                SelAircraft = comboAircraftModel.SelectedItem.ToString();
                SelSoftware = comboAircraftSoft.SelectedItem.ToString();

                CheckModules();
            }
            else
            {
                SelManufacturer = "";
                SelAircraft = "";
                SelSoftware = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FsLVar lvar = MSFSVariableServices.LVars["S_OH_PNEUMATIC_WING_ANTI_ICE"];
            if (lvar != null)
            {
                double newVal = 0;
                if (double.TryParse("1", out newVal))
                {
                    lvar.SetValue(newVal);
                }
            }
        }

    }
}
