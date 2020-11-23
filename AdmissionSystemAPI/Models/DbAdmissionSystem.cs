using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystemAPI.Models
{
    public class DbAdmissionSystem: IdentityDbContext<User,Role,Guid>
    {
        public DbAdmissionSystem(DbContextOptions<DbAdmissionSystem> db) : base(db)
        {

        }


        public DbSet<EducationSystem> EducationSystems { get; set; }
        public DbSet<SchoolInfo> SchoolInfos { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<ExamInfo> ExamInfos { get; set; }
        public DbSet<AdmissionClass> AdmissionClasses { get; set; }
        public DbSet<Result> Results { get; set; }
        //
        public DbSet<StudentInfo> StudentInfos { get; set; }
        public DbSet<PreviousSchoolInfo> PreviousSchoolInfos { get; set; }
        public DbSet<StudentSubjectGPA> StudentSubjectGPAs { get; set; }
        //
        public DbSet<ApplyForm> ApplyForms { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<AdmitCard> AdmitCards { get; set; }
        //

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
    //
    public class Role : IdentityRole<Guid>
    {
    }
    public class User : IdentityUser<Guid>
    {
    }
    //Enum Part
    public enum group { Science = 1, Humanities, BusinessStudies, All }
    public enum shift { Morning, Day, Both }
    public enum subjects { Bangla = 1, English, Mathmetics }
    //School Part
    public class SchoolInfo
    {
        public int Id { get; set; }
        public int EIIN { get; set; }
        public int ParentId { get; set; }
        public string SchoolName { get; set; }
        public string TokenCode { get; set; }
        [ForeignKey("EducationSystem")]
        public int EduSystem { get; set; }
        //[ForeignKey("Address")]
        //public int AddressInfo { get; set; }
        public string Address { get; set; }
        public DateTime SchoolRegDate { get; set; }
        public string PrincipalSeal { get; set; }
        public string PrincipalSigneture { get; set; }
        public group Group { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public shift ShiftName { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string Logo { get; set; }

        public virtual EducationSystem EducationSystem { get; set; }
        //public virtual Address Address { get; set; }


        //public virtual ICollection<Token> Tokens { get; set; }
        public virtual ICollection<AdmissionClass> AdmissionClasses { get; set; }
        public virtual ICollection<ApplyForm> ApplyForms { get; set; }
        public virtual ICollection<Notice> Notices { get; set; }
    }
    public class EducationSystem
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<SchoolInfo> SchoolInfos { get; set; }
    }
    public class AdmissionClass
    {
        public int Id { get; set; }
        [ForeignKey("SchoolInfo")]
        public int SchoolId { get; set; }
        public shift ShiftName { get; set; }
        public string Class { get; set; }
        public int NumberOfSeat { get; set; }

        public virtual SchoolInfo SchoolInfo { get; set; }

        public virtual ICollection<ApplyForm> ApplyForms { get; set; }
        public virtual ICollection<Notice> Notices { get; set; }
    }
    public class Notice
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string NoticeId { get; set; }
        [ForeignKey("SchoolInfo")]
        public int SchoolId { get; set; }
        [ForeignKey("AdmissionClass")]
        public int AdmissionClas { get; set; }
        public shift Shift { get; set; }
        public int AvailableSeat { get; set; }
        public DateTime StartApplyDate { get; set; }
        public DateTime LastApplyDate { get; set; }
        public DateTime NoticeDate { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime ExamDateOrLotteryDate { get; set; }

        public virtual SchoolInfo SchoolInfo { get; set; }
        public virtual AdmissionClass AdmissionClass { get; set; }

    }
    public class ExamInfo
    {
        public int Id { get; set; }
        [ForeignKey("Notice")]
        public int NoticeId { get; set; }
        public DateTime ExamDate { get; set; }
        public decimal TotalMarks { get; set; }
        public decimal PassMark { get; set; }

        public virtual Notice Notice { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
    public class Result : IValidatableObject
    {
        public int Id { get; set; }
        public string TokenCode { get; set; }
        public int TokenNumber { get; set; }
        //[ForeignKey("ExamInfo")]
        //public int ExamInfoId { get; set; }
        [Required]
        public decimal TotalScore { get; set; }
        [Required]
        public bool IsSelected { get; set; }
        [Required]
        public string Merit { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public shift Shift { get; set; }
        public string Year { get; set; }

        //public virtual Token Token { get; set; }
        public virtual ExamInfo ExamInfo { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Merit == null || Merit == string.Empty)
            {
                errors.Add(new ValidationResult(
                "A value is required for the Merit property"));
            }
            if (Shift == 0)
            {
                errors.Add(new ValidationResult(
                "A value is required for the Shift property"));
            }
            if (Year == null || Year == string.Empty)
            {
                errors.Add(new ValidationResult(
                "A value is required for the Year property"));
            }
            if (TotalScore == 0)
            {
                errors.Add(new ValidationResult(
                "A value is required for the TotalScore property"));
            }
            else if (TotalScore < 1 || TotalScore > 100)
            {
                errors.Add(new ValidationResult("The TotalScore value is out of range"));
            }
            
            return errors;
        }


    }
    //Student Part
    public class StudentInfo:IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string StudentName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }
        [Required]
        public int BirthCertificateID { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Height { get; set; }
        public string BloodGroup { get; set; }
        [Required]
        public string FatherName { get; set; }
        public string FatherOccupation { get; set; }
        public string FatherPhone { get; set; }
        [Required]
        public string MotherName { get; set; }
        public string MotherOccupation { get; set; }
        public string MotherPhone { get; set; }
        [Required]
        public string GardianName { get; set; }
        public string GardianOccupation { get; set; }
        public string GardianPhone { get; set; }
        [EmailAddress]
       //[Remote(action: "VerifyEmail", controller: "StudentInfoes")]
        public string Email { get; set; }
        public string PresentAddress { get; set; }
        public string ParmanentAddress { get; set; }
        [StringLength(11,ErrorMessage = "Phone length can't be more than 11.")]
        public string ContuctNumber { get; set; }
        //[ScaffoldColumn(false)]
        public string Photo { get; set; }
        //[ScaffoldColumn(false)]
        public string Signature { get; set; }
        //[ScaffoldColumn(false)]
        public DateTime StudentRegDate { get; set; }
        public virtual ICollection<ApplyForm> ApplyForms { get; set; }
        public virtual ICollection<StudentSubjectGPA> StudentSubjectGPAs { get; set; }
        public virtual ICollection<PreviousSchoolInfo> PreviousSchoolInfos { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (DOB <= DateTime.Now.AddYears(-5))
            {
                errors.Add(new ValidationResult(
                "Date of Birth Must be greater then 5 years"));
            }

            
            return errors;
        }
    }

    public class PreviousSchoolInfo : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        public string PreviousExam { get; set; }
        [Required]
        public string Board { get; set; }
        public string PreviousSchool { get; set; }
        [Required]
        public int Roll { get; set; }
        public int RegistrationNumber { get; set; }
        [Required]
        public int PassingYear { get; set; }
        [Required]
        public Decimal ResultGPA { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual StudentInfo StudentInfos { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (ResultGPA > 5 || ResultGPA < 1)
            {
                errors.Add(new ValidationResult(
                "GPA Must be between 0 to 5.00"));
            }
            if (Roll < 1)
            {
                errors.Add(new ValidationResult(
                "Roll must be positive number"));
            }
            if (PassingYear < 1)
            {
                errors.Add(new ValidationResult(
                "Passing Year must be positive number"));
            }
            if (PreviousExam == null)
            {
                errors.Add(new ValidationResult(
                "Please Provide Previous Exam Name"));
            }
            if (Board == null)
            {
                errors.Add(new ValidationResult(
                "Please Provide Board Name"));
            }
            return errors;
        }
    }
    public class StudentSubjectGPA
    {
        public int Id { get; set; }
        public subjects Name { get; set; }
        public decimal GPA { get; set; }
        public decimal MaxGPA { get; set; }
        [ForeignKey("StudentInfo")]
        public int StudentId { get; set; }
        public virtual StudentInfo StudentInfo { get; set; }
    }
    //Application Part
    public class ApplyForm : IValidatableObject
    {
        public int Id { get; set; }
        [ForeignKey("SchoolInfo")]
        public int SchoolId { get; set; }
        [ForeignKey("AdmissionClass")]
        public int AdmissionClas { get; set; }
        [ForeignKey("StudentInfo")]
        public int StudentId { get; set; }
        [Required]
        public string SchoolTokenCode { get; set; }
        [Required]
        public int TokenNum { get; set; }
        [Required]
        public shift Shift { get; set; }
        [Required]
        public group Group { get; set; }
        public DateTime ApplyDate { get; set; }
        public bool IsEdit { get; set; }
        public bool PaymentStatus { get; set; }

        public virtual SchoolInfo SchoolInfo { get; set; }
        public virtual AdmissionClass AdmissionClass { get; set; }
        public virtual StudentInfo StudentInfo { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<AdmitCard> AdmitCards { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Group == 0)
            {
                errors.Add(new ValidationResult(
                "A value is required for the Merit property"));
            }
            if (Shift == 0)
            {
                errors.Add(new ValidationResult(
                "A value is required for the Shift property"));
            }
            if (ApplyDate < DateTime.Now || ApplyDate < DateTime.Now)
            {
                errors.Add(new ValidationResult(
                "ApplyDate Must be present time"));
            }
            if (TokenNum == 0 || TokenNum < 1)
            {
                errors.Add(new ValidationResult(
                "Token Number Must Be Positive"));
            }
            return errors;
        }

    }
    public class Payment : IValidatableObject
    {
        public int Id { get; set; }
        public string Method { get; set; }
        [ForeignKey("ApplyForm")]
        public int ApplyId { get; set; }
        public decimal Amount { get; set; }
        public decimal SchoolAmount { get; set; }
        public string SchTrxID { get; set; }
        public decimal ChargeAmount { get; set; }
        public string ChaTrxID { get; set; }


        public virtual ApplyForm ApplyForm { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Method == null)
            {
                errors.Add(new ValidationResult(
                "A value is required for the Method property"));
            }
            if (Amount == 0 & Amount < 1)
            {
                errors.Add(new ValidationResult(
                "Amount must be positive number"));
            }
            if (SchoolAmount == 0 & SchoolAmount < 1)
            {
                errors.Add(new ValidationResult(
                "Amount must be positive number"));
            }
            if (ChargeAmount == 0 & ChargeAmount < 1)
            {
                errors.Add(new ValidationResult(
                "Amount must be positive number"));
            }
            return errors;
        }
    }
    public class AdmitCard
    {
        public int Id { get; set; }
        [ForeignKey("ApplyForm")]
        public int ApplicationID { get; set; }
        public DateTime IssueDate { get; set; }

        public virtual ApplyForm ApplyForm { get; set; }

    }


}
