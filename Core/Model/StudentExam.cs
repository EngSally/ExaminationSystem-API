namespace ExaminationSystem.Core.Model
{
	public class StudentExam
	{
		public ApplicationUser Student { get; set; }
		public Exam Exam { get; set; }
		public string Grad { get; set; }
		public DateTime Created { get; set; }

	}
}
