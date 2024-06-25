﻿

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Base.Entites
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;



    }
}