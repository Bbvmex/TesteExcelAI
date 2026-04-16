using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VbeAddin.AI;

namespace VbeAddin.UI
{
    [ComVisible(true)]
    [Guid("B2C3D4E5-F6A7-8901-BCDE-F12345678901")]
    [ProgId("VbeAddin.AiAssistantControl")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public partial class AiAssistantControl : UserControl
    {
        private LlmApiClient _client;

        public AiAssistantControl()
        {
            InitializeComponent();
            InitClient();
        }

        private void InitClient()
        {
            try
            {
                var config = AddinConfig.Load();
                _client = new LlmApiClient(config.BaseUrl, config.Model);
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Config error: {ex.Message}";
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string question = txtQuestion.Text.Trim();
            if (string.IsNullOrEmpty(question)) return;
            if (_client == null)
            {
                txtAnswer.Text = "[Error] LLM client not initialized. Check config.json.";
                return;
            }

            btnSend.Enabled = false;
            lblStatus.Text = "Thinking...";
            txtAnswer.Text = string.Empty;

            try
            {
                string answer = await _client.AskAsync(question);
                txtAnswer.Text = answer;
                lblStatus.Text = "Ready";
            }
            catch (Exception ex)
            {
                txtAnswer.Text = $"[Error] {ex.Message}";
                lblStatus.Text = "Error";
            }
            finally
            {
                btnSend.Enabled = true;
            }
        }
    }
}
