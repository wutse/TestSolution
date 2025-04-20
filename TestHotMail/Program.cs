using MailKit.Net.Pop3;
using MailKit.Security;

namespace TestHotMail
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string email = "ersffsre@hotmail.com";
            string password = "!u#i@U7,+Q)[mPNU";
            string host = "outlook.office365.com"; // Hotmail 的 POP3 伺服器
            int port = 995; // POP3 over SSL 的預設埠


            using (var client = new Pop3Client())
            {
                try
                {
                    // 忽略 SSL 憑證驗證（僅用於測試環境）
                    client.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    // 連線到 POP3 伺服器
                    await client.ConnectAsync(host, port, SecureSocketOptions.SslOnConnect);

                    // 驗證帳號密碼
                    await client.AuthenticateAsync(email, password);

                    Console.WriteLine("Authentication successful!");

                    // 獲取郵件數量
                    int messageCount = client.Count;
                    Console.WriteLine($"Total messages: {messageCount}");

                    // 讀取最近的 5 封郵件
                    for (int i = Math.Max(0, messageCount - 5); i < messageCount; i++)
                    {
                        var message = await client.GetMessageAsync(i);
                        Console.WriteLine($"Subject: {message.Subject}");
                        Console.WriteLine($"From: {string.Join(", ", message.From)}");
                        Console.WriteLine($"Date: {message.Date}");
                        Console.WriteLine(new string('-', 50));
                    }

                    // 斷開連線
                    await client.DisconnectAsync(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while reading emails.");
                    Console.WriteLine(ex.Message);
                }
            }

            Console.ReadLine();
        }
    }
    //public class EmailReader
    //{
    //    private readonly string email;
    //    private readonly string password;

    //    public EmailReader(string email, string password)
    //    {
    //        this.email = email;
    //        this.password = password;
    //    }

    //    public async Task ReadEmailsAsync(string host, int port)
    //    {
    //        try
    //        {
    //            using (var client = new ImapClient(new ProtocolLogger(Console.OpenStandardOutput())))
    //            using (var cancel = new CancellationTokenSource())
    //            {
    //                client.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

    //                await client.ConnectAsync(host, port, SecureSocketOptions.SslOnConnect, cancel.Token);

    //                // Remove unnecessary authentication mechanisms
    //                client.AuthenticationMechanisms.Remove("XOAUTH2");

    //                await client.AuthenticateAsync(email, password, cancel.Token);

    //                var inbox = client.Inbox;
    //                await inbox.OpenAsync(FolderAccess.ReadOnly, cancel.Token);

    //                var uniqueIds = await inbox.SearchAsync(SearchQuery.NotSeen, cancel.Token);

    //                foreach (var uniqueId in uniqueIds.OrderByDescending(id => id.Id))
    //                {
    //                    var message = await inbox.GetMessageAsync(uniqueId, cancel.Token);

    //                    Console.WriteLine($"Subject: {message.Subject}");
    //                    Console.WriteLine($"From: {string.Join(", ", message.From.Mailboxes.Select(m => m.Address))}");
    //                    Console.WriteLine($"Date: {message.Date}");
    //                    Console.WriteLine($"Body: {message.TextBody}");
    //                    Console.WriteLine(new string('-', 50));
    //                }

    //                await client.DisconnectAsync(true, cancel.Token);
    //            }
    //        }
    //        catch (MailKit.Security.AuthenticationException ex)
    //        {
    //            Console.WriteLine("Authentication failed.");
    //            Console.WriteLine(ex.Message);
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine("An error occurred while reading emails.");
    //            Console.WriteLine(ex.Message);
    //        }
    //    }
    //}
}
