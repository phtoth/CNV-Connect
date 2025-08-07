using FSUIPC;
using System.Text.Json;

namespace CNV_Connect
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        // Variáveis de controle
        bool SerialState = false;
        bool SIMState = false;

        // Lista de fabricantes e aeronaves
        string[] ManufacturerList = [];

        // Lista de aeronaves e módulos
        List<string> AircraftList = new List<string>();
        List<string> ModuleList = new List<string>();

        // Variáveis selecionadas pelo usuário
        // Selected variables by the user
        string SelManufacturer = "";
        string SelAircraft = "";
        string SelSoftware = "";

        // Lista de módulos de hardware
        // List of hardware modules
        List<HWModules> HardwareList = new List<HWModules>();

        // Dicionários para armazenar os comandos de entrada e saída
        // Dictionaries to store input and output commands
        Dictionary<int, string> InputList = new Dictionary<int, string>();
        Dictionary<int, string> OutputList = new Dictionary<int, string>();

        // Evento de carregamento do formulário
        // Form load event
        private void frmMain_Load(object sender, EventArgs e)
        {
            // Carrega as portas seriais disponíveis e aeronaves
            // Loads the available serial ports and aircrafts
            LoadComPorts();

            // Desabilita os botões de conexão com o Simulador e Placa
            // Disables the buttons for connecting to the Simulator and Board
            btnConnectSerial.Enabled = false;
            //btnConnectSIM.Enabled = false;
            LoadAircrafts();

            // Inicia o serço de variáveis do MSFS
            // Initializes the MSFS variable service
            InitMSFSServices();

            // Configura o evento de envio de dados para o simulador
            DataQueue.SendToSimDelegate = SendToSim;
        }

        // Método que envia os dados para o simulador
        // Method that sends data to the simulator
        private void SendToSim(int CodeMap, int Value)
        {
            // Verifica se o código de mapeamento existe na lista de entradas
            // Checks if the mapping code exists in the input list
            if (InputList.ContainsKey(CodeMap))
            {
               string Command = InputList[CodeMap];
                if (Command != null)
                {
                    FsLVar lvar = MSFSVariableServices.LVars[Command];
                    lvar.SetValue(Value);
                }
            }
        }

        // Carrega as aeronaves disponíveis no diretório Boards
        // Loads the available aircrafts from the Boards directory
        // ToDo: Melhorar o carregamento das aeronaves para evitar problemas de performance
        // ToDo: Improve the loading of aircrafts to avoid performance issues
        // ToDo: Talvez mudar o formato de armazenamento das aeronaves
        // ToDo: Maybe change the storage format of aircrafts
        private void LoadAircrafts()
        {
            // Carrega o diretório de fabricantes de aeronaves
            // Loads the aircraft manufacturers directory
            string[] ManufacturerListTemp = System.IO.Directory.GetDirectories("../../../Boards/");

            // Verifica se o diretório de fabricantes está vazio
            // Checks if the manufacturers directory is empty
            int ManufacturerListSize = ManufacturerListTemp.Length;
            string[] AircraftListTemp = [];

            // Se houver fabricantes, adiciona-os ao ComboBox e à lista de aeronaves
            // If there are manufacturers, adds them to the ComboBox and the aircraft list
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

        // Evento de clique do botão de teste de conexão
        // Click event for the connection test button
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
                // Se não conseguir se comunicar com a placa, exibe uma mensagem de erro
                // If it fails to communicate with the board, displays an error message
                string message = "Nenhuma Placa Encontrada.";
                string title = "Erro";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, title, buttons, MessageBoxIcon.Asterisk);
            }


        }


        // Evento de clique do botão de conexão serial
        // Click event for the serial connection button
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

        // Timer que verifica se o CNV-Connect está ativo
        // Timer that checks if CNV-Connect is active
        private void tmrStayinAlive_Tick(object sender, EventArgs e)
        {

        }

        // Método que inicializa os serviços de variáveis do MSFS
        // Method that initializes the MSFS variable services
        private void InitMSFSServices()
        {
            // Handle events
            //MSFSVariableServices.OnLogEntryReceived += VS_OnLogEntryReceived; // Fired when the WASM module sends a log entry
            //MSFSVariableServices.OnVariableListChanged += VS_VariableListChanged; // Fired when the list of available variables is changed
            //MSFSVariableServices.OnValuesChanged += VS_OnValuesChanged; // Fired when any LVAR value changes
            // Initialise and start

            // Verifica quando devariável LVAR é alterada
            // Checks when the LVAR variable is changed
            MSFSVariableServices.OnValuesChanged += VS_OnValuesChanged;

            // Inicializa e inicia o serviço de variáveis do MSFS
            // Initializes and starts the MSFS variable service
            MSFSVariableServices.Init();

            // Seta o nível de log
            // Sets the log level
            MSFSVariableServices.LogLevel = LOGLEVEL.LOG_LEVEL_INFO; // Set the level of logging

            // Inicia o serviço de variáveis do MSFS
            // Starts the MSFS variable service
            MSFSVariableServices.Start();

            // Obtém a lista de variáveis LVAR disponíveis
            // Gets the list of available LVAR variables
            List<string> lvarNames = new List<string>(MSFSVariableServices.LVars.Names);

            // Ordena a lista de variáveis LVAR
            // Sorts the list of LVAR variables
            lvarNames.Sort();
        }

        // Método que atualiza o valor em tempo real
        // Method that updates the real-time value
        private void VS_OnValuesChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(reportChangedLVars));
        }

        // Método que reporta as variáveis LVAR alteradas
        // Method that reports the changed LVAR variables
        private void reportChangedLVars()
        {
            // Varre a lista de variáveis LVAR alteradas, que foram mapeadas no dicionário OutputList
            // Iterates through the list of changed LVAR variables that were mapped in the OutputList dictionary

            foreach (FsLVar lvar in MSFSVariableServices.LVarsChanged)
            {
                if (OutputList.ContainsValue(lvar.Name))
                {
                    // Tratamos o valor recebido do LVAR
                    // We process the value received from the LVAR
                    string Data = ProcessMessage(lvar.Value.ToString("F6"));

                    // Obtemos o código de mapeamento do LVAR a partir do dicionário OutputList
                    // We get the mapping code of the LVAR from the OutputList dictionary
                    int CodeMap = OutputList.FirstOrDefault(x => x.Value == lvar.Name).Key;

                    // Aqui quramos o código de mapeamento em duas partes
                    // Here we break the mapping code into two parts
                    // Como o código é um inteiro com 5 dígitos, dividimos em duas partes, dividindo por 1000
                    // Since the code is a 5-digit integer, we split it into two parts, dividing by 1000

                    double BreakData = CodeMap / 1000;

                    // a Part_01 é a parte inteira do código de mapeamento
                    // Part_01 is the integer part of the mapping code
                    // a Part_02 é o resto da divisão do código de mapeamento por 1000
                    // Part_02 is the remainder of the mapping code divided by 1000
                    int Part_01 = (int)BreakData;
                    int Part_02 = CodeMap - (Part_01 * 1000);

                    // Monta a mensagem a ser enviada para o CNV-Connect
                    // Builds the message to be sent to CNV-Connect
                    string Message = String.Concat("##", Part_01.ToString(), "#", Part_02.ToString(), "#", Data, "##");

                    // Envia a mensagem para o CNV-Connect
                    // Sends the message to CNV-Connect
                    SerialComm.SerialSend(Message);
                }
            }
        }

        // Método que trata os dados recebidos do CNV-Connect
        // Method that processes the data received from CNV-Connect
        public static string ProcessMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return "";

            // Separar a parte inteira da decimal
            string[] partes = message.Split(',');

            if (partes.Length == 1)
            {
                // Sem vírgula, apenas inteiro
                return partes[0];
            }

            string parteInteira = partes[0];
            string parteDecimal = partes[1];

            // Se todos os dígitos decimais são zero
            if (parteDecimal.All(c => c == '0'))
            {
                return parteInteira;
            }
            else
            {
                // Remove zeros à direita da parte decimal
                string decimalSemZeros = parteDecimal.TrimEnd('0');
                return $"{parteInteira},{decimalSemZeros}";
            }
        }



        // Método de Conexão com o Simulador
        // Method to connect to the simulator
        private void SimConnect()
        {
            // Leitura dos módulos de hardware
            // Reading the hardware modules
            string ModulePath = "../../../Boards/" + SelManufacturer + "/" + SelAircraft + "/" + SelSoftware + "/";
            string[] ModulesList = System.IO.Directory.GetFiles(ModulePath);

            // Carrega os módulos de hardware encontrados
            // Loads the found hardware modules
            // ToDo: Melhorar o carregamento dos módulos para evitar problemas de performance
            // ToDo: Improve the loading of modules to avoid performance issues

            foreach (string Module in ModulesList)
            {
                string jsonContent = File.ReadAllText(Module);
                HWModules NewModule = JsonSerializer.Deserialize<HWModules>(jsonContent)!;

                if (NewModule.AircraftManufacturer == SelManufacturer && NewModule.AircraftModel == SelAircraft && NewModule.AircraftVariant == SelSoftware)
                {
                    HardwareList.Add(NewModule);
                }
                else
                {
                    MessageBox.Show("Erro ao carregar módulo de Hardware. Arquivo com erro de configuração.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (HardwareList.Count > 0)
            {
                foreach (var HWModules in HardwareList)
                {
                    foreach (var HW_Item in HWModules.BoardData.EnumerateObject())
                    {
                        string CHeckInOut = HW_Item.ToString();
                        if (CHeckInOut.Contains("Input"))
                        {
                            JsonDocument doc_01 = JsonDocument.Parse(HW_Item.Value.GetRawText());
                            JsonElement root_01 = doc_01.RootElement;

                            foreach (JsonProperty Item in root_01.EnumerateObject())
                            {
                                JsonDocument doc_02 = JsonDocument.Parse(Item.Value.GetRawText());
                                JsonElement root_02 = doc_02.RootElement;

                                // Ensure the value is not null before parsing
                                string? mapCodeString = root_02.GetProperty("Map_Code").GetString();
                                if (!string.IsNullOrEmpty(mapCodeString))
                                {
                                    int InputMapCode = int.Parse(mapCodeString);
                                    string? InputCommand = root_02.GetProperty("Map_CMD").GetString();

                                    if (!InputList.ContainsKey(InputMapCode) && InputCommand != null)
                                    {
                                        InputList.Add(InputMapCode, InputCommand);
                                    }
                                }
                            }

                        }
                        else if (CHeckInOut.Contains("Output"))
                        {
                            JsonDocument doc_01 = JsonDocument.Parse(HW_Item.Value.GetRawText());
                            JsonElement root_01 = doc_01.RootElement;

                            foreach (JsonProperty Item in root_01.EnumerateObject())
                            {
                                JsonDocument doc_02 = JsonDocument.Parse(Item.Value.GetRawText());
                                JsonElement root_02 = doc_02.RootElement;

                                foreach (JsonProperty OutputItem in root_02.EnumerateObject())
                                {
                                    var CommandMap = JsonDocument.Parse(OutputItem.Value.GetRawText());
                                    var CodeMap = JsonDocument.Parse(OutputItem.Name.ToString());

                                    string OutputCommand = CommandMap.RootElement.ToString();
                                    int OutputMapCode = int.Parse(CodeMap.RootElement.GetRawText());

                                    if (!OutputList.ContainsKey(OutputMapCode))
                                    {
                                        OutputList.Add(OutputMapCode, OutputCommand);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Evento de clique do botão de conexão com o Simulador
        // Click event for the simulator connection button
        private void btnConnectSIM_Click(object sender, EventArgs e)
        {
            DataQueue.Turret.Start();
            Thread.Sleep(100);
            //SerialComm.SerialSend("ARE_YOU_STILL_THERE");

            // desabilita os botões

            btnConnectSIM.Enabled = false;

            comboAircraftManufacturer.Enabled = false;
            comboAircraftModel.Enabled = false;
            comboAircraftSoft.Enabled = false;

            comboConnSerial.Enabled = false;

            btnConnTest.Enabled = false;
            btnConnectSerial.Enabled = false;

            SimConnect();

        }

        // Evento de mudança de seleção do ComboBox de Modelos de Aeronaves
        // ComboBox selection change event for Aircraft Models
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
        // Checks if the simulation module is installed
        private void CheckModules()
        {
            // Lista os módulos instalados no diretório Modules
            // Lists the installed modules in the Modules directory
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
                        // Updated the line to handle possible null values safely by using the null-coalescing operator.
                        // Atualizado para lidar com valores nulos possíveis usando o operador de coalescência nula.
                        string boardType = boardTypeElement.GetString() ?? string.Empty;

                        // Verifica o tipo de placa e marca o checkbox correspondente
                        // Checks the board type and marks the corresponding checkbox
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

        // Evento de mudança de seleção do ComboBox de Fabricantes de Aeronaves
        // ComboBox selection change event for Aircraft Manufacturers
        private void comboAircraftManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Limpa o ComboBox de Modelos de Aeronaves e Fabricantes
            // Clears the Aircraft Models and Manufacturers ComboBoxes
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

        // Limpa os checkboxes dos módulos de hardware
        // Clears the checkboxes of the hardware modules
        private void ClearModules()
        {
            cbOverhead.Checked = false;
            cbRadio.Checked = false;
        }

        // Evento de mudança de seleção do ComboBox de Softwares de Aeronaves
        // ComboBox selection change event for Aircraft Software
        private void comboAircraftSoft_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboAircraftSoft.Items.Count > 0)
            {
                SelManufacturer = comboAircraftManufacturer.SelectedItem?.ToString()!;
                SelAircraft = comboAircraftModel.SelectedItem?.ToString()!;
                SelSoftware = comboAircraftSoft.SelectedItem?.ToString()!;


                CheckModules();
            }
            else
            {
                SelManufacturer = "";
                SelAircraft = "";
                SelSoftware = "";
            }
        }
    }
}
