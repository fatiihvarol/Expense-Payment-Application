using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Business.Cqrs;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Enum;
using WebBase.Response;
using WebSchema;
using WebBase.Helper;

namespace Web.Business.Query.ReportQuery
{
    public class ReportQueryHandler :
        IRequestHandler<GelMyReportQuery, ApiResponse<List<ReportResponse>>>,
        IRequestHandler<GetCompanyReportQueryByPeriod, ApiResponse<List<ReportResponse>>>,
        IRequestHandler<GetEmployeeReportQuery, ApiResponse<List<ReportResponse>>>,
        IRequestHandler<GetCompanyReportQueryByStatusAndPeriod, ApiResponse<List<ReportResponse>>>,
        IRequestHandler<GetCompanyReportQueryByStatus, ApiResponse<List<ReportResponse>>>,
        IRequestHandler<GetAllPaymentsReportQuery, ApiResponse<List<PaymentResponse>>>

    {
        private readonly VbDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly PeriodHelper _periodHelper;

        public ReportQueryHandler(VbDbContext dbContext, IMapper mapper, PeriodHelper periodHelper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _periodHelper = periodHelper;
        }

        public async Task<ApiResponse<List<ReportResponse>>> Handle(GelMyReportQuery request,
            CancellationToken cancellationToken)
        {
            var report = await _dbContext.Set<Expense>()
                .Include(x => x.Address)
                .Include(x => x.Category)
                .Include(x => x.Payment)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId == request.Id)
                .ToListAsync(cancellationToken);

            if (!report.Any())
                return new ApiResponse<List<ReportResponse>>("not found any expenses");

            var mapped = _mapper.Map<List<Expense>, List<ReportResponse>>(report);

            return new ApiResponse<List<ReportResponse>>(mapped);
        }

        public async Task<ApiResponse<List<ReportResponse>>> Handle(GetCompanyReportQueryByPeriod request,
            CancellationToken cancellationToken)
        {
            var timePeriod = CalculatePeriod(request.ReportTimePeriod);

            var expenses = await _dbContext.Set<Expense>()
                .Include(x => x.Address)
                .Include(x => x.Category)
                .Include(x => x.Payment)
                .Include(x => x.Employee)
                .Where(x => x.InsertDate >= timePeriod)
                .ToListAsync(cancellationToken);

            if (!expenses.Any())
                return new ApiResponse<List<ReportResponse>>("not found any expenses");

            var mapped = _mapper.Map<List<Expense>, List<ReportResponse>>(expenses);

            return new ApiResponse<List<ReportResponse>>(mapped);
        }

        public async Task<ApiResponse<List<ReportResponse>>> Handle(GetEmployeeReportQuery request,
            CancellationToken cancellationToken)
        {
            var employee = await _dbContext.Set<Employee>()
                .FirstOrDefaultAsync(x => x.Id == request.EmployeeId, cancellationToken);

            if (employee is null)
                return new ApiResponse<List<ReportResponse>>("employee not found with id");

            var period = CalculatePeriod(request.ReportTimePeriod);

            var expenses = await _dbContext.Set<Expense>()
                .Include(x => x.Address)
                .Include(x => x.Category)
                .Include(x => x.Payment)
                .Include(x => x.Employee)
                .Where(x => x.InsertDate >= period && x.EmployeeId == request.EmployeeId)
                .ToListAsync(cancellationToken);

            if (!expenses.Any())
                return new ApiResponse<List<ReportResponse>>("not found any expenses");
            
            var mapped = _mapper.Map<List<Expense>, List<ReportResponse>>(expenses);

            return new ApiResponse<List<ReportResponse>>(mapped);
        }

        public async Task<ApiResponse<List<ReportResponse>>> Handle(GetCompanyReportQueryByStatusAndPeriod request,
            CancellationToken cancellationToken)
        {
            var period = CalculatePeriod(request.ReportTimePeriod);

            var expenses = await _dbContext.Set<Expense>()
                .Include(x => x.Address)
                .Include(x => x.Category)
                .Include(x => x.Payment)
                .Include(x => x.Employee)
                .Where(z => z.InsertDate >= period)
                .Where(y => y.Status == request.ExpenseStatus)
                .ToListAsync(cancellationToken);
            
            if (!expenses.Any())
                return new ApiResponse<List<ReportResponse>>("not found any expenses");
            
            
            var mapped = _mapper.Map<List<Expense>, List<ReportResponse>>(expenses);

            return new ApiResponse<List<ReportResponse>>(mapped);
        }

        
        public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetAllPaymentsReportQuery request, CancellationToken cancellationToken)
        {
            var payments = await _dbContext.Set<Payment>()
                .Include(x => x.Expense)
                .ToListAsync(cancellationToken);
            if (!payments.Any())
                return new ApiResponse<List<PaymentResponse>>("not found any expenses");
            var mapped = _mapper.Map<List<Payment>, List<PaymentResponse>>(payments);
            return new ApiResponse<List<PaymentResponse>>(mapped);
        }

        public async Task<ApiResponse<List<ReportResponse>>> Handle(GetCompanyReportQueryByStatus request, CancellationToken cancellationToken)
        {

            var expenses = await _dbContext.Set<Expense>()
                .Include(x => x.Address)
                .Include(x => x.Category)
                .Include(x => x.Payment)
                .Include(x => x.Employee)
                .Where(y => y.Status == request.ExpenseStatus)
                .ToListAsync(cancellationToken);
            
            if (!expenses.Any())
                return new ApiResponse<List<ReportResponse>>("not found any expenses");
            
            
            var mapped = _mapper.Map<List<Expense>, List<ReportResponse>>(expenses);
            
            return new ApiResponse<List<ReportResponse>>(mapped);
        }
        
        
        
        
        
        
        
        
        
        
        
        private DateTime CalculatePeriod(ReportTimePeriod request)
        {
            var date = _periodHelper.Calculate(request);
            return date;
        }
    }
}