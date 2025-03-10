﻿using System.Text.Json.Serialization;

namespace TodoApi.Models.Entities
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public UserModel User { get; set; }
    }
}
