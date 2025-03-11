
namespace WebAPISampleProjectUsingVS2022.Models.Repository_Pattern
{
    public class EmpRepository : IEmpRepository
    {
        private readonly List<Emp> _empList;
        public EmpRepository()
        {
            _empList = new List<Emp> {
                new Emp{EmpNo=1,EmpName="Shivaa",job="IT", Salary=3000 },
                new Emp{ EmpNo=2,EmpName="Ramaa",job="Clerk",Salary=800 },
                new Emp{EmpNo=3,EmpName="Krishnaa",job="Manager",Salary=8008 }
            };

        }
        public void AddEmp(Emp emp)
        {
            emp.EmpNo=_empList.Max(x=>x.EmpNo)+1;
           _empList.Add(emp);
        }

        public void DeleteEmp(int EmpNo)
        {
           var emp=GetEmpById(EmpNo);
            if(emp!=null)
            {
                _empList.Remove(emp);

            }
        }

        public bool EmpExists(int EmpNo)
        {
            return _empList.Any(x => x.EmpNo==EmpNo);
        }

        public IEnumerable<Emp> GetAllEmp()
        {
            return _empList;
        }

        public Emp GetEmpById(int EmpNo)
        {
            return _empList.FirstOrDefault(x => x.EmpNo == EmpNo);
        }

        public void UpdateEmp(Emp emp)
        {
            
            var existingEmp = GetEmpById(emp.EmpNo);
            _empList.Remove(existingEmp);
            if (existingEmp != null)
            {
                existingEmp.EmpName = emp.EmpName;
                existingEmp.Salary = emp.Salary;
                existingEmp.job = emp.job;
            }
            _empList.Add(existingEmp);
        }

        public Emp GetEmpByIdOrName(int EmpNo, string name)
        {
            return _empList.FirstOrDefault(x => x.EmpNo == EmpNo ||  x.EmpName==name);
        }
    }
}
