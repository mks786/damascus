﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Damascus.Message
{
    public interface IUniqueCall
    {
        string UniqueCallId { get; set; }
    }

    public class Contact
    {
        [JsonProperty(PropertyName = "unique_id")]
        public string ContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Dictionary<string, string> ToDict()
        {
            return new Dictionary<string, string>()
            {
                {"unique_id", ContactId},
                {"contact_name", Name},
                {"email", Email},
                {"phone", Phone},
            };
        }

        public static Contact FromDict(Dictionary<string, string>values)
        {
            return new Contact()
            {
                ContactId = values["unique_id"],
                Name = values["contact_name"],
                Email = values["email"],
                Phone = values["phone"]
            };
        }
    }

    public class ReducedInviteInput : StepInput
    {
        public string InviteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Where { get; set; }
    }

    public class InviteInput : StepInput, IUniqueCall
    {
        [JsonProperty(PropertyName = "uniquecall_id")]
        public string UniqueCallId { get; set; }

        [JsonProperty(PropertyName = "unique_id")]
        public string InviteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Where { get; set; }
        public Dictionary<string, string> ExtraContextData { get; set; }
        public SharingOptions SharingOptions { get; set; }

        [JsonProperty(PropertyName = "cancel_email_template")]
        public EmailTemplate CancelEmailTemplate { get; set; }
        [JsonProperty(PropertyName = "email_template")]
        public EmailTemplate EmailTemplate { get; set; }
        [JsonProperty(PropertyName = "response_email_template")]
        public EmailTemplate ResponseEmailTemplate { get; set; }
        [JsonProperty(PropertyName = "sms_template")]
        public BodyTemplate SmsTemplate { get; set; }
        [JsonProperty(PropertyName = "voice_template")]
        public BodyTemplate PhoneCallTemplate { get; set; }

        public Dictionary<string, string> ToDict()
        {
            return new Dictionary<string, string>()
            {
                {"invite_unique_id", InviteId},
                {"title", Title},
                {"description", Description},
                {"start", Start.ToString()},
                {"end", (End!=null)?End.ToString():null},
                {"where", Where},
                {"email_template", (this.EmailTemplate!=null)?this.EmailTemplate.ToString():""},
                {"response_email_template", (this.ResponseEmailTemplate!=null)?this.ResponseEmailTemplate.ToString():""},
                {"sms_template", (this.SmsTemplate!=null)?this.SmsTemplate.ToString():""},
            };
        }

        public static InviteInput FromDict(Dictionary<string,string> value)
        {
            return new InviteInput()
            {
                InviteId = value["invite_unique_id"],
                Title = value["title"],
                Description = value["description"],
                Start = DateTime.Parse(value["start"]),
                End =  (string.IsNullOrEmpty(value["end"]))? new DateTime?() :  DateTime.Parse(value["end"]),
                Where = value["where"],
                EmailTemplate = Message.EmailTemplate.FromString(value["email_template"]),
                ResponseEmailTemplate = Message.EmailTemplate.FromString(value["response_email_template"]),
                SmsTemplate = Message.BodyTemplate.FromString(value["sms_template"]),
            };
        }
    }

    public class InviteAttendeesInput : StepInput, IUniqueCall
    {
        [JsonProperty(PropertyName = "uniquecall_id")]
        public string UniqueCallId { get; set; }
        [JsonProperty(PropertyName = "invite_unique_id")]
        public string InviteId { get; set; }
        public bool SmsDisabled { get; set; }
        public bool PhoneCallDisabled { get; set; }
        public bool EmailDisabled { get; set; }
        [JsonProperty(PropertyName = "attendees")]
        public List<Contact> Attendees { get; set; }
        
        
    }

    public class InviteCancelInput : StepInput
    {
        public string InviteId { get; set; }
    }

    public class Location
    {
        public string Address { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public override string ToString()
        {
            var suite = this.Suite ?? "";
            var address = this.Address ?? "";
            var state = this.State ?? "";
            var city = this.City ?? "";
            var zip = this.Zip ?? "";

            return address + " " + suite + " " + city + " ," + state + "  " + zip;
        }
    }

    public class SharingOptions
    {
        public string FacebookAccessToken { get; set; }
        public string TwitterAccessToken { get; set; }

    }
}
