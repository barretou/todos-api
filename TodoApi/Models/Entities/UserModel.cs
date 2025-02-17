namespace TodoApi.Models.Entities
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<TaskModel> Tasks { get; set; }
    }
}
