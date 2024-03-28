namespace EmloyeeManagementSystem.ViewModel
{
    public class AdminViewModel
    {
        public string UserId { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

    }

    public class WorkingHours
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public string UserId { get; set; }

    }

    public class UserDTO
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
