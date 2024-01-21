using AutoMapper;
using Web.Data.Entity;
using WebSchema;

namespace Web.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<ApplicationUser, ApplicationUserResponse>();
        CreateMap<ApplicationUserRequest, ApplicationUser>();

        CreateMap<EmployeeRequest, Employee>();
        CreateMap<Employee, EmployeeResponse>();
        CreateMap<Expense, ExpenseResponse>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment));

        CreateMap<Address, AddressResponse>();
        CreateMap<AddressRequest, Address>();

        CreateMap<ExpenseCategory, ExpenseCategoryResponse>();
        CreateMap<ExpenseRequest, Expense>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        
        CreateMap<Payment, PaymentResponse>();
        CreateMap<ExpenseCategory, ExpenseCategoryResponse>();

        CreateMap<Payment, PaymentResponse>();

    }
}