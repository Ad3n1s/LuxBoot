namespace LuxBoot.Models
{
    public class AttackItem
    {
        public int Id { get; set; }

        public UserApp userId { get; set; }
        public string AttackType { get; set; }
        public string IpAddress { get; set; }
        public string Port { get; set; }
        public string Time { get; set; }
        public string TimeLeft { get; set; }

    }
}
