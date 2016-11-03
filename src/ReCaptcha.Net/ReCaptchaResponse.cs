using System.Runtime.Serialization;

namespace ReCaptcha.Net
{
    [DataContract]
    public class ReCaptchaResponse
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "challenge_ts")]
        public string ChallengeTimeStampRaw { get; set; }

        [DataMember(Name = "hostname")]
        public string Hostname { get; set; }

        [DataMember(Name = "error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
