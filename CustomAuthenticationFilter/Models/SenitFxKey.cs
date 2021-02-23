using System;

namespace CustomAuthenticationFilter.Models
{
    public class SenitFxKey
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}