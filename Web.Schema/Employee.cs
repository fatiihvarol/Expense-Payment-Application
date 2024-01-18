using System.Text.Json.Serialization;
using Web.Data.Entity;

namespace WebSchema;

public class EmployeeRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public int ApplicationUserId { get; set; }  
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string IBAN { get; set; }
    
}

public class EmployeeResponse
{
    public int Id { get; set; }
    public int ApplicationUserId { get; set; } 
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmployeeNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string IBAN { get; set; }
    public DateTime LastActivityDate { get; set; }
    
    
    public List<ExpenseResponse> Expenses { get; set; }
}