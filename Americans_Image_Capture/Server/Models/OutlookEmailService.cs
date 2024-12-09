using System.Net.Mail;

namespace Americans_Image_Capture.Server.Models
{
    public class OutlookEmailService
    {

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }


        public MailMessage MailConfig { get; set; }
        public SmtpClient simpleMailTransferProtocol { get; set; }
        public OutlookEmailService()
        {
            MailConfig = new MailMessage
            {
                //new MailAddress("Maag.ERG.Mfgdocs@maag.com"),
                To = {
                new MailAddress("Maag.ERG.Mfgdocs@maag.com") },
                Sender = new MailAddress("sai.krishna@axiscades.in", "Me"),
                //maag.egr.techdocs@maag.com
                From = new MailAddress("sai.krishna@axiscades.in", "Client"),
                Subject = "GALA Doc Testing",
                Body = "Hip Hip Hurry...!!!! \r\n Agglomerate Catcher\r\nMechanical Check Sheet for All Models and Sizes Saved.!!!!!!!!!!!!!!!!!!!",
                IsBodyHtml = true,
            };

            // Email Configuration
            simpleMailTransferProtocol = new SmtpClient
            {
                //smtp.dovercorporation.com
                Host = "smtp.office365.com",
                Port = 587,
                Credentials = new System.Net.NetworkCredential("sai.krishna@axiscades.in", "dDsS**052023"),  // Sender Email User Id Password
                EnableSsl = true
            };

        }

        //public void SendEmail(Project project, Checklist checklist)
        //{

        //    string htmlString = @$"<html> 
        //              <body> 
        //              <p>Dear Mr/Ms,</p> 
        //              <p> In {project.ProjectNo} CheckList {checklist.Name} Survey Sucessfully Updated </p> 
        //              <p>Sincerely,<br>-MAAG-Tech-Docs</br></p> 
        //              </body> 
        //              </html> 
        //             ";
        //    string subject = $"CheckSheet :{checklist.Name}" + $" Survey Completed";
        //    MailConfig.Subject = subject;
        //    MailConfig.Body = htmlString;



        //    simpleMailTransferProtocol.Send(MailConfig);
        //}

    }
}
