namespace lapora_ktm_api.Models
{
	public class Report
	{
		public int Id { get; set; }
		public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        //public Student Student { get; set; }

        public Report(int id, string title, string description, string status, DateTime createdAt)
		{
			Id = id;
			Title = title;
			Description = description;
			Status = status;
			CreatedAt = createdAt;
			//Student = student;
		}
	}
}