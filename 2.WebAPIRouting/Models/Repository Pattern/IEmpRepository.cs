namespace WebAPISampleProjectUsingVS2022.Models.Repository_Pattern
{
    public interface IEmpRepository
    {
        IEnumerable<Emp> GetAllEmp();
        Emp GetEmpById(int EmpNo);
        Emp GetEmpByIdOrName(int EmpNo, string name);
        void AddEmp(Emp emp);
        void UpdateEmp(Emp emp);
        void DeleteEmp(int EmpNo);
        bool EmpExists(int EmpNo);
    }
}
