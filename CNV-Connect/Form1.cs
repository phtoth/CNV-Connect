namespace CNV_Connect
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadComPorts();
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

            // Chama o teste passando como parâmetro a porta serial
            SerialComm.TestConnection(comboConnSerial.Text);

            // Volta o cursor para o padrão
            Cursor.Current = Cursors.Default;

            // Se o teste conseguir se comunicar com a placa, traz os dados coletados via serial e popula o textbox
            // Caso contrário, exibe uma mensagem de aviso
            if (SerialComm.Board_Version != "")
            {
                lblBoardVersion.Text = SerialComm.Board_Version;
            }
            else
            {
                string message = "Nenhuma Placa Encontrada.";
                string title = "Erro";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, title, buttons, MessageBoxIcon.Asterisk);
            }


        }
    }
}
