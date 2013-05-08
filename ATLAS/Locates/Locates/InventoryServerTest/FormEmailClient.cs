using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace StockLoan.Inventory
{
    public partial class FormEmailClient : Form
    {
        public FormEmailClient()
        {
            InitializeComponent();
        }

        private const string MAIL_FROM = "LWu@Penson.com";

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (textBoxTo.Text.Trim() == String.Empty)
            {
                MessageBox.Show("To cann't be empty", "Email Error");
                return;
            }

            if (textBoxSubject.Text.Trim() == String.Empty)
            {
                if (DialogResult.No == MessageBox.Show("Warning: Do you want send Mail without subject?", "Warning", MessageBoxButtons.YesNo))
                    return;
            }
            
            MailMessage mail = new  MailMessage();
            try
            {

                String[] addes = textBoxTo.Text.Split(';');
                foreach (String str in addes)
                    mail.To.Add(new MailAddress(str));

                if (textBoxCc.Text != String.Empty)
                {
                    addes = this.textBoxCc.Text.Split(';');
                    foreach (String str in addes)
                        mail.CC.Add(new MailAddress(str));
                }

                if (textBoxBcc.Text != String.Empty)
                {
                    addes = textBoxBcc.Text.Split(';');
                    foreach (String str in addes)
                        mail.Bcc.Add(new MailAddress(str));
                }

                mail.Subject = textBoxSubject.Text;
                mail.Body = textBoxBody.Text;
                mail.Priority = ((ComboItem<MailPriority>)comboBoxPriority.SelectedItem).Data;
                mail.From = new MailAddress(MAIL_FROM);

                // set attachements of mail
                if (textBoxAttachment.Text.Trim() != String.Empty)
                {
                    String[] str = textBoxAttachment.Text.Split(';');
                    if (str.Length > 0)
                    {
                        for (int i = 0; i < str.Length; i++)
                        {
                            mail.Attachments.Add(new Attachment(str[i]));
                        }
                    }
                }
                EmailClient client = new EmailClient(this.textBoxExchangeServer.Text, this.textBoxUsername.Text, this.textBoxPassword.Text);
                client.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void labelAttachment_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "C:\\";
            dlg.Filter = "All files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.Multiselect)
                {
                    foreach (string strFile in dlg.FileNames)
                    {
                        if( textBoxAttachment.Text.Trim() == String.Empty)
                        {
                            this.textBoxAttachment.Text +=  strFile;
                        }
                        else
                        {
                            this.textBoxAttachment.Text +=  ";" + strFile;
                        }
                    }
                }
                else
                {
                    if (textBoxAttachment.Text.Trim() == String.Empty)
                    {
                        this.textBoxAttachment.Text += dlg.FileName;
                    }
                    else
                    {
                        this.textBoxAttachment.Text += ";" + dlg.FileName;
                    }
                }
            }
        }

        private void FormEmailSend_Load(object sender, EventArgs e)
        {
            this.textBoxExchangeServer.Text ="EXPENVS1.penson.com";
            this.textBoxFileLocation.Text = "C:\\";
            this.textBoxSearchFrom.Text = "lwu@penson.com";
            this.textBoxSearchSubject.Text = "Attachment Test";
            this.textBoxTo.Text = "lwu@penson.com";

            try
            {
                ComboItem<EmailSearchCriteria.EmailDateSearchOption> dateOption = new ComboItem<EmailSearchCriteria.EmailDateSearchOption>(EmailSearchCriteria.EmailDateSearchOption.NOSEARCH, "NoSearch");
                this.comboBoxDateOption.Items.Add(dateOption);
                dateOption = new ComboItem<EmailSearchCriteria.EmailDateSearchOption>(EmailSearchCriteria.EmailDateSearchOption.SENTBEFORE, "SentBefore");
                this.comboBoxDateOption.Items.Add(dateOption);
                dateOption = new ComboItem<EmailSearchCriteria.EmailDateSearchOption>(EmailSearchCriteria.EmailDateSearchOption.SENTSINCE, "SentSince");
                this.comboBoxDateOption.Items.Add(dateOption);
                this.comboBoxDateOption.SelectedIndex = 0;

                ComboItem<MailPriority> priority = new ComboItem<MailPriority>(MailPriority.High, "High");
                this.comboBoxPriority.Items.Add(priority);
                priority = new ComboItem<MailPriority>(MailPriority.Low, "Low");
                this.comboBoxPriority.Items.Add(priority);
                priority = new ComboItem<MailPriority>(MailPriority.Normal, "Normal");
                this.comboBoxPriority.Items.Add(priority);
                this.comboBoxPriority.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void buttonRetrieve_Click(object sender, EventArgs e)
        {
            try
            {
                this.richTextBoxLog.Clear();
                EmailClient client = new EmailClient(textBoxExchangeServer.Text, this.textBoxUsername.Text, this.textBoxPassword.Text);
                EmailSearchCriteria criteria = new EmailSearchCriteria();
                criteria.From = this.textBoxSearchFrom.Text;
                criteria.Subject = this.textBoxSearchSubject.Text;
                criteria.EmailDateOption = ((ComboItem<EmailSearchCriteria.EmailDateSearchOption>)this.comboBoxDateOption.SelectedItem).Data;

                if (criteria.EmailDateOption != EmailSearchCriteria.EmailDateSearchOption.NOSEARCH)
                    criteria.Date = this.dateTimePickerSearchDate.Value;
                List<MailMessage> lstMails = new List<MailMessage>();

                lstMails = client.Retrieve(criteria, "INBOX");
                foreach (MailMessage mail in lstMails)
                {
                    richTextBoxLog.AppendText("From:" + mail.From.ToString() + "\n");
                    richTextBoxLog.AppendText("To:" + mail.To.ToString() + "\n");
                    richTextBoxLog.AppendText("Cc:" + mail.CC.ToString() + "\n");
                    richTextBoxLog.AppendText("Subject:" + mail.Subject + "\n");
                    if (mail.Attachments.Count > 0)
                    {
                        client.SaveAttachment(mail, textBoxFileLocation.Text);
                        richTextBoxLog.AppendText(String.Format("Attachments{0}: ", mail.Attachments.Count));
                        foreach (Attachment att in mail.Attachments)
                        {
                            richTextBoxLog.AppendText(att.Name + ";");
                        }
                        richTextBoxLog.AppendText("\n");
                    }

                    richTextBoxLog.AppendText("Body:\n" + mail.Body + "\n");

                    //                Forward(mail);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void Forward(MailMessage mail)
        {
            EmailClient client = new EmailClient(this.textBoxExchangeServer.Text, this.textBoxUsername.Text, this.textBoxPassword.Text);
            mail.To.Clear();
            mail.To.Add(new MailAddress("lwu@penson.com"));
            mail.CC.Clear();
            mail.Bcc.Clear();
            client.Send(mail);
        }
    }

    public class ComboItem <T>
    {
        private T _data;
        public T Data
        {
            get
            {
                return _data;
            }
        }

        private String _name;

        public override string ToString()
        {
            return _name;
        }

        public ComboItem(T data, String name)
        {
            _data = data;
            _name = name;
        }
    }
}