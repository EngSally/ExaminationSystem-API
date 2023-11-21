namespace ExaminationSystem.Core.Model
{
	public class Question
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public string Choice1 { get; set; } = null!;
		public string Choice2 { get; set; } = null!;
		public string Choice3 { get; set; } = null!;
		public string Choice4 { get; set; } = null!;
		public string Answer { get; set; } = null!;
		public  Exam Exam { get; set; }

	}
}
