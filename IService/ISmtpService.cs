
namespace IService
{
    public interface ISmtpService
    {
        public string HostAddress { get; set; }
        public int HostPort { get; set; }
        public string HostUsername { get; set; }
        public string HostPassword { get; set; }
        public string SenderName { get; set; }
    }
}
