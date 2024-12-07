using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Lab6Security
{
    public partial class Form1 : Form
    {
        private RSACryptoServiceProvider rsaProvider;

        public Form1()
        {
            txtPublicKey = new TextBox();
            txtMessage = new TextBox();
            txtEncryptedMessage = new TextBox();
            txtDecryptedMessage = new TextBox();
            txtFullKeyPair = new TextBox();
            txtImportKey = new TextBox();

            InitializeComponent();
            InitializeRSA();
        }

        private void InitializeRSA()
        {
            rsaProvider = new RSACryptoServiceProvider();
        }

        private void btnGenerateKeys_Click(object sender, EventArgs e)
        {
            try
            {
               
                string publicKey = rsaProvider.ToXmlString(false);
                txtPublicKey.Text = publicKey;

               
                string fullKeyPair = rsaProvider.ToXmlString(true);
                txtFullKeyPair.Text = fullKeyPair;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating keys: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (string.IsNullOrEmpty(txtPublicKey.Text))
                {
                    MessageBox.Show("Please generate or import a public key first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               
                RSACryptoServiceProvider encryptRsa = new RSACryptoServiceProvider();
                encryptRsa.FromXmlString(txtPublicKey.Text);

               
                byte[] messageBytes = Encoding.Unicode.GetBytes(txtMessage.Text);

               
                byte[] encryptedBytes = encryptRsa.Encrypt(messageBytes, false);

               
                txtEncryptedMessage.Text = Convert.ToBase64String(encryptedBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Encryption error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
               
                byte[] encryptedBytes = Convert.FromBase64String(txtEncryptedMessage.Text);

               
                byte[] decryptedBytes = rsaProvider.Decrypt(encryptedBytes, false);

               
                string decryptedMessage = Encoding.Unicode.GetString(decryptedBytes);

               
                txtDecryptedMessage.Text = decryptedMessage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Decryption error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportKey_Click(object sender, EventArgs e)
        {
            try
            {
               
                rsaProvider.FromXmlString(txtImportKey.Text);
                MessageBox.Show("Key imported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing key: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void InitializeComponent()
        {
           
            this.Text = "RSA Encryption System";
            this.Width = 600;
            this.Height = 900;

           
            Label lblFullKeyPair = new Label
            {
                Text = "Full Key Pair:",
                Location = new System.Drawing.Point(10, 180),
                Width = 150
            };
            txtFullKeyPair.Location = new System.Drawing.Point(10, 200);
            txtFullKeyPair.Width = 550;
            txtFullKeyPair.Multiline = true;
            txtFullKeyPair.Height = 100;
            txtFullKeyPair.ReadOnly = true;

           
            Label lblPublicKey = new Label
            {
                Text = "Public Key:",
                Location = new System.Drawing.Point(10, 10),
                Width = 150
            };
            txtPublicKey.Location = new System.Drawing.Point(10, 30);
            txtPublicKey.Width = 550;
            txtPublicKey.Multiline = true;
            txtPublicKey.Height = 100;
            txtPublicKey.ReadOnly = true;

            Button btnGenerateKeys = new Button
            {
                Text = "Generate Keys",
                Location = new System.Drawing.Point(10, 310)
            };
            btnGenerateKeys.Width = 150;
            btnGenerateKeys.Height = 30;
            btnGenerateKeys.Click += btnGenerateKeys_Click;

           
            Label lblMessage = new Label
            {
                Text = "Message to Encrypt:",
                Location = new System.Drawing.Point(10, 350),
                Width = 150
            };
            txtMessage.Location = new System.Drawing.Point(10, 370);
            txtMessage.Width = 550;
            txtMessage.Height = 50;
            txtMessage.Multiline = true;

            Button btnEncrypt = new Button
            {
                Text = "Encrypt",
                Location = new System.Drawing.Point(10, 430),
                Height = 30
            };
            btnEncrypt.Click += btnEncrypt_Click;

           
            Label lblEncryptedMessage = new Label
            {
                Text = "Encrypted Message:",
                Location = new System.Drawing.Point(10, 470),
                Width = 150
            };
            txtEncryptedMessage.Location = new System.Drawing.Point(10, 490);
            txtEncryptedMessage.Width = 550;
            txtEncryptedMessage.Height = 50;
            txtEncryptedMessage.Multiline = true;

           
            Button btnDecrypt = new Button
            {
                Text = "Decrypt",
                Location = new System.Drawing.Point(10, 550),
                Height = 30
            };
            btnDecrypt.Click += btnDecrypt_Click;

           
            Label lblDecryptedMessage = new Label
            {
                Text = "Decrypted Message:",
                Location = new System.Drawing.Point(10, 590),
                Width = 150
            };
            txtDecryptedMessage.Location = new System.Drawing.Point(10, 610);
            txtDecryptedMessage.Width = 550;
            txtDecryptedMessage.Height = 50;
            txtDecryptedMessage.Multiline = true;

           
            Label lblImportKey = new Label
            {
                Text = "Import Key (XML Format):",
                Location = new System.Drawing.Point(10, 670),
                Width = 200
            };
            txtImportKey.Location = new System.Drawing.Point(10, 690);
            txtImportKey.Width = 550;
            txtImportKey.Height = 50;
            txtImportKey.Multiline = true;

            Button btnImportKey = new Button
            {
                Text = "Import Key",
                Location = new System.Drawing.Point(10, 750),
                Height = 30
            };
            btnImportKey.Click += btnImportKey_Click;

           
            this.Controls.AddRange(new Control[] {
                lblPublicKey, txtPublicKey,
                lblFullKeyPair, txtFullKeyPair,
                btnGenerateKeys,
                lblMessage, txtMessage, btnEncrypt,
                lblEncryptedMessage, txtEncryptedMessage, btnDecrypt,
                lblDecryptedMessage, txtDecryptedMessage,
                lblImportKey, txtImportKey, btnImportKey
            });
        }

       
        private TextBox txtPublicKey;
        private TextBox txtMessage;
        private TextBox txtEncryptedMessage;
        private TextBox txtDecryptedMessage;
        private TextBox txtFullKeyPair;
        private TextBox txtImportKey;
    }
}
