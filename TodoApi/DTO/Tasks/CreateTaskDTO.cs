﻿namespace TodoApi.DTO.Tasks
{
    public class CreateTaskDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
