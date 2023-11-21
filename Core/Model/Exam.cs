namespace ExaminationSystem.Core.Model
{
	public class Exam
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<Question> Questions { get; set; } = new List<Question>();
	}
}
