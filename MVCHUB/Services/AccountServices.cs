using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using AutoMapper;
using System.Data.Entity;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using MVCHUB.Interfaces;
using MVCHUB.Models;

namespace MVCHUB.Services
{
    public class AccountServices : IAccountService
    {

        protected IMapper iMapper;
        protected DBContext _context;


        //Constructor
        public AccountServices()
        {       
            //MapConfig();
            _context = new DBContext();
        }

        //Get list of all clients
        public async Task<IEnumerable<string>> getAllUsers()
        {

            var names = await _context.Account.Select(g => g.userName).ToListAsync();
   
            if (names != null)
            {
                return names;
            }
            throw new Exception("No user found");
        }
        public async Task<IEnumerable<string>> getAllUsersSql()
        {

            string queryString = "select Username, Password from Account";
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            List<string> names = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        names.Add(reader["Username"].ToString()+":"+ reader["Password"].ToString());
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            if (names != null)
            {
                return names;
            }
            throw new Exception("No user found");
        }  
        public async Task<AccountModel> createNewAccount(AccountModel data)
        {
                Account Account = new Account();
                DateTime date = DateTime.Now;
                if (data != null)
                {
                Account.userName=data.UserName;
                Account.createDate = date;
                Account.expirationDate = date.AddYears(1); 
                Account.resourceKey = "";
                Account.Password = data.Password;
                Account.firstName = data.FirstName;
                Account.lastName = data.LastName;
                Account.fullName = data.FirstName+ " " + data.LastName;
                Account.Email = data.Email;
                data.FullName = data.FirstName + " " + data.LastName;
                var UserAccount = await _context.Account.FirstOrDefaultAsync(x => x.userName == data.UserName);

                    if (UserAccount != null)
                    {
                        throw new Exception("User with same username already exists");
                    }

                    _context.Account.Add(Account);

                    _context.Contexts.SaveChanges();
                    return data;
                }
                throw new Exception("No data entered");
           
        }
        public async Task<String> accountValidation(AccountModel data)
        {
        
            if (data != null)
            {
                
                var UserAccount = await _context.Account.FirstOrDefaultAsync(x => x.userName == data.UserName);

                if (UserAccount != null)
                {
                    var password = await _context.Account.FirstOrDefaultAsync(x => x.Password == data.Password);
                    if(password != null)
                    {
                        var fullName = await _context.Account.Where(x => x.userName == data.UserName).Select(x=> x.fullName).FirstOrDefaultAsync();
                        return fullName;
                    }
                    else
                    {
                        return null;
                    }
                }
                throw new Exception("User doesn't exist");



            }
            throw new Exception("No data entered");

        }
        public async Task<bool> DeleteAccount(int id)
        {

            string queryString = "Delete Account WHERE id = '" + id + "'";
            string connectionString = "Server=fyplab.database.windows.net;Database=LSHUB;User Id=wei;Password=Predator423;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

            }
            return true;


        }

   
        ////Get all stress test belonging to a client
        //public async Task<IEnumerable<StressTestModel>> getAllClientStressTests(string name)
        //{
        //    var clientId = await _context.clients.Where(n => n.SnippClientName == name).Select(x => x.Id).FirstOrDefaultAsync();

        //    if (clientId > 0)
        //    {
        //        var allStressTestsByClient = _context.stressTest.Where(c => c.ClientId == clientId).ToList();

        //        if (allStressTestsByClient.Any())
        //        {
        //            return mapStressModel(allStressTestsByClient);
        //        }
        //        else
        //        {
        //            throw new Exception("No Stress Test Available");
        //        }
        //    }
        //    throw new Exception("No client found");
        //}

        ////Get client's stress test by testID
        //public async Task<StressTestModel> getClientStressTestById(string testName, int stressTestId)
        //{
        //    var clientIdFromClient = await _context.clients.Where(n => n.SnippClientName == testName).Select(x => x.Id).FirstOrDefaultAsync();

        //    if (clientIdFromClient > 0)
        //    {
        //        var individualStressTest = await _context.stressTest.Where(c => c.Id == stressTestId && c.ClientId == clientIdFromClient).FirstOrDefaultAsync();

        //        if (individualStressTest != null)
        //        {
        //            return singleModelMapping(individualStressTest);
        //        }
        //        else
        //        {
        //            throw new Exception("No Stress Test Available");
        //        }
        //    }
        //    throw new Exception("No client found");
        //}

        ////Create new stress test
        //public async Task<StressTestModel> createNewStressTest(string Clientname, StressTestModel data)
        //{
        //    var clientId = await _context.clients.Where(n => n.SnippClientName == Clientname).Select(x => x.Id).FirstOrDefaultAsync();

        //    if (clientId > 0)
        //    {
        //        DateTime date = DateTime.Now;
        //        if (data != null)
        //        {
        //            data.Path = pythonPath + Clientname + "/";
        //            data.MasterScript = Clientname + "-MasterScript" + date.Year + date.Month + date.Day + date.Minute + date.Second;
        //            data.ChildScript = "PythonChild";
        //            data.DateTime = date;

        //            data.StatusName = await _context.status.Where(s => s.StatusName == "Active")
        //                .Select(x => x.StatusName).FirstOrDefaultAsync();

        //            StressTest dbModel = iMapper.Map<StressTestModel, StressTest>(data);

        //            dbModel.ClientId = clientId;

        //            dbModel.TypeId = await _context.type.Where(s => s.TypeName == data.TypeName)
        //                .Select(a => a.TypeId).FirstOrDefaultAsync();

        //            dbModel.StatusId = await _context.status.Where(s => s.StatusName == data.StatusName)
        //                .Select(x => x.StatusId).FirstOrDefaultAsync();

        //            var TestName = await _context.stressTest.FirstOrDefaultAsync(x => x.ClientId == clientId
        //            && x.TestName.Equals(data.TestName, StringComparison.InvariantCultureIgnoreCase)
        //            && x.StressTestType.TypeName.Equals(data.TypeName, StringComparison.InvariantCultureIgnoreCase));

        //            if (TestName != null)
        //            {
        //                throw new Exception("Test with same name already exists");
        //            }

        //            _context.stressTest.Add(dbModel);

        //            _context.context.SaveChanges();
        //            return data;
        //        }
        //        throw new Exception("No data entered");
        //    }
        //    throw new Exception("No client found");
        //}
        //public async Task<string> execStressTest(string Clientname, ExecuteModel data)
        //{
        //    var clientId = await _context.clients.Where(n => n.SnippClientName == Clientname).Select(x => x.Id).FirstOrDefaultAsync();

        //    if (clientId > 0)
        //    {
        //        List<string> files = new List<string>();
        //        List<string> datas = new List<string>();
        //        DateTime date = DateTime.Now;
        //        string server = data.Server;
        //        string database = data.Database;
        //        string userName = data.UserName;
        //        string password = data.Password;
        //        resultPath = resultPath + Clientname + "_" + date.ToString("ddMMyyyy_hhMMss");//+ ".txt";              
        //        string connectionString = "Server=" + server + ";Database=" + database + ";User Id=" + userName + ";Password=" + password + ";MultipleActiveResultSets=true;";

        //        if (data != null)
        //        {
        //            SqlConnection sqlConnection = new SqlConnection(connectionString);
        //            SqlCommand cmd = new SqlCommand();
        //            SqlDataReader reader;
        //            cmd.CommandText = data.Query;
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Connection = sqlConnection;


        //            sqlConnection.Open();

        //            for (int a = 0; a < data.Parallel; a++)
        //            {
        //                resultPath = resultPath + "_" + a + ".txt";
        //                files.Add(resultPath);
        //                using (var tw = new StreamWriter(resultPath, true))
        //                {
        //                    tw.WriteLine("Start Time: " + DateTime.Now);
        //                }
        //            }


        //            Parallel.For(0, data.Parallel, o =>
        //            {
        //                for (int i = 0; i < data.Iterative; i++)
        //                {
        //                    using (reader = cmd.ExecuteReader())
        //                    {
        //                        try
        //                        {
        //                            while (reader.Read())
        //                            {
        //                                for (int x = 0; x < data.Columns.Count; x++)
        //                                {
        //                                    datas.Add(reader[data.Columns[x]].ToString() + " , ");
        //                                }

        //                            }
        //                        }
        //                        finally
        //                        {

        //                            reader.Close();
        //                        }
        //                    }
        //                }
        //            });
        //            for (int a = 0; a < data.Parallel; a++)
        //            {
        //                File.AppendAllText(files[a], datas[a]);
        //                File.AppendAllText(files[a], "\r\n");
        //            }
        //            File.AppendAllText(resultPath, "Finish Time : " + DateTime.Now);

        //            sqlConnection.Close();
        //            return "Success";
        //        }
        //        throw new Exception("No data entered");
        //    }
        //    throw new Exception("No client found");
        //}
        ////Update Stress test data
        //public async Task<StressTestModel> updateStressTest(string Clientname, int stressTestId, StressTestModel update)
        //{
        //    DateTime date = DateTime.Now;
        //    if (Clientname != null || update != null || stressTestId > 0)
        //    {
        //        StressTest dbModel = await _context.stressTest.Where(x => x.Id == stressTestId && x.ClientConfiguration.SnippClientName == Clientname).FirstOrDefaultAsync();
        //        if (dbModel != null)
        //        {
        //            var typeId = await _context.type.Where(x => x.TypeName == update.TypeName)
        //                .Select(y => y.TypeId).FirstOrDefaultAsync();

        //            update.Path = pythonPath + Clientname + "/";
        //            update.MasterScript = Clientname + "-MasterScript" + date.Year + date.Month + date.Day + date.Minute + date.Second;
        //            update.ChildScript = "PythonChild";
        //            dbModel.TypeId = typeId;
        //            dbModel = iMapper.Map(update, dbModel);

        //            _context.context.SaveChanges();

        //            return update;
        //        }
        //        throw new Exception("No Stress Test found");
        //    }
        //    throw new Exception("No client found");
        //}

        ////Disable Stress test 
        //public async Task<StressTestModel> disableStressTest(string Clientname, int stressTestId, StressTestModel update)
        //{
        //    var clientId = await _context.clients.Where(x => x.SnippClientName == Clientname).Select(x => x.Id).FirstOrDefaultAsync();

        //    if (clientId > 0 && update != null && stressTestId > 0)
        //    {
        //        StressTest dbModel = await _context.stressTest.Where(x => x.Id == stressTestId && x.ClientConfiguration.SnippClientName == Clientname).FirstOrDefaultAsync();
        //        if (dbModel != null)
        //        {
        //            update.StatusName = await _context.stressTest.Where(x => x.StressTestStatus.StatusName == "InActive")
        //                .Select(x => x.StressTestStatus.StatusName).FirstOrDefaultAsync();
        //            dbModel.StatusId = await _context.status.Where(s => s.StatusName == "InActive").Select(x => x.StatusId).FirstOrDefaultAsync();
        //            _context.context.SaveChanges();

        //            return update;
        //        }
        //        throw new Exception("No Stress Test found");
        //    }
        //    throw new Exception("No client found");

        //}

        ////Activate Stress test 
        //public async Task<StressTestModel> activateStressTest(string Clientname, int stressTestId, StressTestModel update)
        //{
        //    var clientId = await _context.clients.Where(x => x.SnippClientName == Clientname).Select(x => x.Id).FirstOrDefaultAsync();

        //    if (clientId > 0 && update != null && stressTestId > 0)
        //    {
        //        StressTest dbModel = await _context.stressTest.Where(x => x.Id == stressTestId && x.ClientConfiguration.SnippClientName == Clientname).FirstOrDefaultAsync();
        //        if (dbModel != null)
        //        {
        //            update.StatusName = await _context.stressTest.Where(x => x.StressTestStatus.StatusName == "Active")
        //                .Select(x => x.StressTestStatus.StatusName).FirstOrDefaultAsync();
        //            dbModel.StatusId = await _context.status.Where(s => s.StatusName == "Active").Select(x => x.StatusId).FirstOrDefaultAsync();
        //            _context.context.SaveChanges();

        //            return update;
        //        }
        //        throw new Exception("No Stress Test found");
        //    }
        //    throw new Exception("No client found");
        //}

        ////Map StressTest Model
        //private IEnumerable<StressTestModel> mapStressModel(IEnumerable<StressTest> StressTestList)
        //{
        //    DateTime TimeNow = DateTime.Now;
        //    List<StressTestModel> stModel = StressTestList.Select(x => new StressTestModel()
        //    {
        //        TestName = x.TestName,
        //        TypeName = x.StressTestType.TypeName,
        //        StatusName = x.StressTestStatus.StatusName,
        //        MasterScript = x.MasterScript,
        //        ChildScript = x.ChildScript,
        //        Extension = x.Extension,
        //        Path = x.Path,
        //        DateTime = x.DateTime
        //    }).ToList();
        //    var name = stModel;
        //    return stModel;
        //}

        ////Single StressTest Model Mapping
        //private StressTestModel singleModelMapping(StressTest stressTest)
        //{
        //    StressTestModel stressTestModel = new StressTestModel();

        //    stressTestModel.TestName = stressTest.TestName;
        //    stressTestModel.TypeName = stressTest.StressTestType.TypeName;
        //    stressTestModel.StatusName = stressTest.StressTestStatus.StatusName;
        //    stressTestModel.MasterScript = stressTest.MasterScript;
        //    stressTestModel.ChildScript = stressTest.ChildScript;
        //    stressTestModel.Extension = stressTest.Extension;
        //    stressTestModel.Path = stressTest.Path;

        //    return stressTestModel;
        //}

        ////Map Configuration
        //private void MapConfig()
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<StressTestModel, StressTest>().ForMember(x => x.Id, x => x.Ignore());
        //    });
        //    iMapper = config.CreateMapper();
        //}
    }
}