using JobProcessingInterface;
using MSXML3;
using System;
using System.IO;
using Tyler.Odyssey.JobProcessing;
using Tyler.Odyssey.Utils;
using RelateCoDefendantCasesJob.Helpers;
using System.Linq;
using RelateCoDefendantCasesJob.Exceptions;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.Serialization.Json;
using Tyler.Odyssey.API.JobTemplate;
using Tyler.Integration.Framework;
using System.Xml.Serialization;
using Tyler.Odyssey.API.Shared;
using Tyler.Integration.General;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace RelateCoDefendantCasesJob
{
  internal class DataProcessor : TaskProcessor
  {
    // Constructor
    public DataProcessor(string SiteID, string JobTaskXML) : base(SiteID, JobTaskXML)
    {
      Logger.WriteToLog("JobTaskXML:\r\n" + JobTaskXML, LogLevel.Basic);

      // New up the context object
      Context = new Context(Logger);

      Logger.WriteToLog("Completed instantiation of context object", LogLevel.Verbose);

      // Retrieve the parameters for the job (which flags to add/remove)
      Context.DeriveParametersFromJobTaskXML(SiteID, JobTaskXML);
      Context.ValidateParameters();

      Logger.WriteToLog("Finished deriving parameters", LogLevel.Verbose);

      // TODO:  Add the code tables that need to be updated to the following function (Context.AddCacheItems())
      Context.AddCacheItems();
      Context.UpdateCache();

      Logger.WriteToLog("Completed cache update.", LogLevel.Verbose);
    }

    // Static constructor
    static DataProcessor()
    {
      Logger = new UtilsLogger(LogManager);
      Logger.WriteToLog("Logger Instantiated", LogLevel.Basic);
    }

    // Destructor
    ~DataProcessor()
    {
      Logger.WriteToLog("Disposing!", LogLevel.Basic);

      if (Context != null)
        Context.Dispose();
    }

    public static IUtilsLogManager LogManager = new UtilsLogManagerBase(Constants.LOG_REGISTRY_KEY);
    public static readonly UtilsLogger Logger;

    public IXMLDOMDocument TaskDocument { get; set; }

    internal Context Context { get; set; }

    public ITYLJobTaskUtility TaskUtility { get; set; }

    private object taskParms;
    public object TaskParms { get { return taskParms; } set { taskParms = value; } }

    public override void Run()
    {
      Logger.WriteToLog("Beginning Run Method", LogLevel.Basic);

      try
      {

        DateTime? dtStart = null;
        DateTime? dtEnd = null;
        //DateTime TempDate;

        DateTime TempStartDate;
        DateTime TempEndDate;

        if (DateTime.TryParse(Context.Parameters.RelativeAsOfDateStart, out TempStartDate))//if success parse the date
        {
          dtStart = TempStartDate;
        }
        else
        {
          Logger.WriteToLog("Error bad scheduling Start Date.", LogLevel.Verbose);
        }

        if (DateTime.TryParse(Context.Parameters.RelativeAsOfDateEnd, out TempEndDate))//if success parse the date
        {
          dtEnd = TempEndDate;
        }
        else
        {
          Logger.WriteToLog("Error bad scheduling end date.", LogLevel.Verbose);
        }

        if (dtStart != null && dtEnd != null)
        {
          Logger.WriteToLog($"Proposed Start Date: {dtStart.ToString()}", LogLevel.Verbose);
          Logger.WriteToLog($"Proposed End Date : {dtEnd.ToString()}", LogLevel.Verbose);


        DataSet ds = GetSqlDataSet(DateTime.Today, DateTime.Today);
        DataTable dataTable = ds.Tables[0];

        Logger.WriteToLog($"DataTable Count: {dataTable.Rows.Count}", LogLevel.Verbose);

        List<object> caseIdValues = new List<object>();
        List<object> codefendantValues = new List<object>();

        var tupleList = new List<Tuple<object, object>> { };

          if (dataTable.Rows.Count > 0)
          {

            foreach (DataRow row in dataTable.Rows)
            {
              caseIdValues.Add(row["CaseID"]);
              codefendantValues.Add(row["CrossReferenceNumber"]);

              tupleList.Add(Tuple.Create(row["CaseID"], row["CrossReferenceNumber"]));
            }

            Logger.WriteToLog($"caseIDValues Count: {caseIdValues.Count}", LogLevel.Verbose);
            Logger.WriteToLog($"codefendantValues Count: {codefendantValues.Count}", LogLevel.Verbose);
            int outerIndex = 0;
            int innerIndex = 0;
            bool matched = false;
            string CrossReferenceNumber = "";
            string MasterCaseID = "";

            //////////////////////////////////////
            //Need to figure out how to use the data I got
            //foreach (String Outercodefendantnumber in codefendantValues)
            //{
            //  String currentNumber = Outercodefendantnumber;

            //  foreach (String InnerCodefendantNumber in codefendantValues) {
            //    if (InnerCodefendantNumber.Equals(Outercodefendantnumber) && outerIndex != innerIndex) {
            //      Logger.WriteToLog("Made it before addrelatedcase api call", LogLevel.Basic);
            //      Logger.WriteToLog("outerIndex value = " + outerIndex.ToString(), LogLevel.Basic);
            //      Logger.WriteToLog("innerIndex value = " + innerIndex.ToString(), LogLevel.Basic);
            //      Logger.WriteToLog("Outer caseID " + caseIdValues.ElementAt(outerIndex).ToString(), LogLevel.Basic);
            //      Logger.WriteToLog("Inner caseID " + caseIdValues.ElementAt(innerIndex).ToString(), LogLevel.Basic);
            //      AddRelatedCase(caseIdValues.ElementAt(outerIndex).ToString(), caseIdValues.ElementAt(innerIndex).ToString());
            //      //caseIdValues.RemoveAt(innerIndex);
            //      //codefendantValues.RemoveAt(innerIndex);
            //      //matched = true;
            //      //continue;
            //    }
            //    innerIndex++;
            //  }
            //  //if (matched) {
            //  //  caseIdValues.RemoveAt(outerIndex);
            //  //  codefendantValues.RemoveAt(outerIndex);
            //  //}
            //  innerIndex = 0;
            //  outerIndex++;

            //}
            ///////////////////////////////////////////////

            List<String> relations = new List<string>();
            foreach (Tuple<object, object> tup in tupleList)
            {
              Logger.WriteToLog("Current loop codefendant number = " + tup.Item2.ToString(), LogLevel.Basic);
              if (!CrossReferenceNumber.Equals(tup.Item2.ToString()))
              {
                if (relations.Count != 0)
                {
                  Logger.WriteToLog("relations count = " + relations.Count.ToString(), LogLevel.Basic);
                  foreach (String outerCaseID in relations)
                  {
                    foreach (String innerCaseID in relations)
                    {
                      if (innerCaseID.Equals(outerCaseID))
                      {
                        continue;
                      }
                      AddRelatedCase(outerCaseID, innerCaseID);
                    }
                  }
                  relations.Clear();
                }
                relations.Add(tup.Item1.ToString());
                CrossReferenceNumber = tup.Item2.ToString();
                continue;
              }
              else
              {
                relations.Add(tup.Item1.ToString());
              }
            }

            if (relations.Count != 0)
            {
              Logger.WriteToLog("relations count = " + relations.Count.ToString(), LogLevel.Basic);
              foreach (String outerCaseID in relations)
              {
                foreach (String innerCaseID in relations)
                {
                  if (innerCaseID.Equals(outerCaseID))
                  {
                    continue;
                  }
                  AddRelatedCase(outerCaseID, innerCaseID);
                }
              }
              relations.Clear();
            }



            var tupleReportList = new List<Tuple<object, object>> { };
            DataSet dsr = GetReportDataSet(DateTime.Today, DateTime.Today);
            DataTable dataTableReport = dsr.Tables[0];

            Logger.WriteToLog($"DataTable Count: {dataTableReport.Rows.Count}", LogLevel.Verbose);


            if (dataTableReport.Rows.Count > 0)
            {

              foreach (DataRow row in dataTableReport.Rows)
              {
                tupleReportList.Add(Tuple.Create(row["CaseNbr"], row["CrossReferenceNumber"]));
              }


              Logger.WriteToLog("---------Could Not Relate-----------", LogLevel.Basic);
              Logger.WriteToLog("CaseNumber:                  DCN:   ", LogLevel.Basic);
              foreach (Tuple<object,object> tup in tupleReportList)
              {
                Logger.WriteToLog($"{tup.Item1.ToString()}                  {tup.Item2.ToString()}", LogLevel.Basic);
              }

            }





            }




        }



      }
      catch (Exception e)
      {
        Console.Write("Errored out");
        Context.Errors.Add(new BaseCustomException(e.Message));
      }

      // TODO: Handle errors we've collected during the job run.
      if (Context.Errors.Count > 0)
      {
        // Add a message to the job indicating that something went wrong.
        AddInformationToJob();

        // Collect errors, write them to a file, and attach the file to the job.
        LogErrors();
      }

      ContinueWithProcessing("Job Completed Successfully");
    }

    // Call with a single API
    private string FindCase()
    {
      Tyler.Odyssey.API.JobTemplate.FindCaseByCaseNumberEntity entity = new Tyler.Odyssey.API.JobTemplate.FindCaseByCaseNumberEntity();
      entity.SetStandardAttributes(int.Parse(Context.Parameters.NodeID), "FindCase", Context.UserID, "FindCase", Context.SiteID);
      entity.CaseNumber = Context.Parameters.CaseNumber;

      OdysseyMessage msg = new OdysseyMessage(entity.ToOdysseyMessageXml(), Context.SiteID);
      Logger.WriteToLog("xml = " + entity.ToOdysseyMessageXml(), LogLevel.Basic);
      MessageHandlerFactory.Instance.ProcessMessage(msg);

      StringReader reader = new StringReader(msg.ResponseDocument.OuterXml);
      XmlSerializer serializer = new XmlSerializer(typeof(Tyler.Odyssey.API.JobTemplate.FindCaseByCaseNumberResultEntity));
      Tyler.Odyssey.API.JobTemplate.FindCaseByCaseNumberResultEntity result = (Tyler.Odyssey.API.JobTemplate.FindCaseByCaseNumberResultEntity)serializer.Deserialize(reader);

      return result.CaseID;
    }

    private AddRelatedCaseResultEntity AddRelatedCase(String CaseID, String RelatedCaseID)
    {
      Logger.WriteToLog("Got CaseID's " + CaseID + " and " + RelatedCaseID, LogLevel.Basic);
      Tyler.Odyssey.API.JobTemplate.AddRelatedCaseEntity entity = new Tyler.Odyssey.API.JobTemplate.AddRelatedCaseEntity();
      entity.SetStandardAttributes(int.Parse("200"), "AddRelatedCase", Context.UserID, "AddRelatedCase", Context.SiteID);
      entity.ReferenceNumber = "AddRelatedCase";
      entity.UserID = "1";
      entity.CaseID = CaseID;
      entity.RelatedCaseID = RelatedCaseID;
      entity.Reason = "CD";


      try
      {
        Logger.WriteToLog("xml = " + entity.ToOdysseyMessageXml(), LogLevel.Basic);
        OdysseyMessage msg = new OdysseyMessage(entity.ToOdysseyMessageXml(), Context.SiteID);
        MessageHandlerFactory.Instance.ProcessMessage(msg);

        StringReader reader = new StringReader(msg.ResponseDocument.OuterXml);
        XmlSerializer serializer = new XmlSerializer(typeof(Tyler.Odyssey.API.JobTemplate.AddRelatedCaseResultEntity));
        Tyler.Odyssey.API.JobTemplate.AddRelatedCaseResultEntity result = (Tyler.Odyssey.API.JobTemplate.AddRelatedCaseResultEntity)serializer.Deserialize(reader);

        return result;
      }
      catch (Exception e)
      {
        Logger.WriteToLog("AddRelatedCase Exception: " + e + '\n' + e.InnerException, LogLevel.Basic);
        Tyler.Odyssey.API.JobTemplate.AddRelatedCaseResultEntity failedObject = new Tyler.Odyssey.API.JobTemplate.AddRelatedCaseResultEntity();
        return failedObject;
      }

    }




    // Process Transaction
    public string ProcessTransaction(string transXml)
    {
      string txnResults = string.Empty;

      try
      {
        OdysseyTransaction txn = new OdysseyTransaction(0, transXml, Context.SiteID);
        TransactionProcessor txnProcessor = new TransactionProcessor();
        txnProcessor.ProcessTransaction(txn);

        if (txn.TransactionRejected)
          throw new Exception(txn.RejectReason);
        else
          if (txn.ResponseDocument != null)
          txnResults = txn.ResponseDocument.OuterXml;
      }
      // if a schema exceptions is thrown, then throw the new exception with the data from the schema error.
      catch (SchemaValidationException svex)
      {
        throw new Exception(svex.ReplacementStrings[0]);
      }
      catch (DataConversionException dcex)
      {
        // check for an xslCodeQuery exception type in the inner exception
        // so we can report a better, more descriptive error 
        if (dcex.InnerException.GetType().Equals(typeof(XslCodeQueryException)))
        {
          XslCodeQueryException xcqe = (XslCodeQueryException)dcex.InnerException;
          throw new Exception(xcqe.ReplacementStrings[0], dcex);
        }
        else
        {
          throw new Exception(dcex.Message);
        }

      }
      catch (Exception ex)
      {
        throw ex;
      }

      return txnResults;
    }





    private DataSet GetSqlDataSet(DateTime? startDate, DateTime? endDate)
    {
      Logger.WriteToLog("Get SQL Data Set", LogLevel.Verbose);
      string QUERY = createSQL(startDate, endDate);

      Logger.WriteToLog("Sql query = " + QUERY, LogLevel.Basic);

      CDBBroker broker = new CDBBroker(Context.SiteID);

      var brokerConnection = broker.GetConnection("Justice");

      DataSet ret = null;

      SqlCommand cmd = new SqlCommand(string.Format(QUERY), brokerConnection as SqlConnection);

      try
      {
        cmd.Connection.Open();

        ret = CDBBroker.LoadDataSet(Context.SiteID, cmd, true);
      }
      finally
      {
        if (cmd != null)
        {
          cmd.Connection.Close();
        }
      }

      return ret;

    }



    private DataSet GetReportDataSet(DateTime? startDate, DateTime? endDate)
    {
      Logger.WriteToLog("Get SQL Data Set", LogLevel.Verbose);
      string QUERY = createSQLForReport(startDate, endDate);

      Logger.WriteToLog("Sql query = " + QUERY, LogLevel.Basic);

      CDBBroker broker = new CDBBroker(Context.SiteID);

      var brokerConnection = broker.GetConnection("Justice");

      DataSet ret = null;

      SqlCommand cmd = new SqlCommand(string.Format(QUERY), brokerConnection as SqlConnection);

      try
      {
        cmd.Connection.Open();

        ret = CDBBroker.LoadDataSet(Context.SiteID, cmd, true);
      }
      finally
      {
        if (cmd != null)
        {
          cmd.Connection.Close();
        }
      }

      return ret;

    }

    private string createSQL(DateTime? startDate, DateTime? endDate)
    {
      Logger.WriteToLog("Create SQL", LogLevel.Verbose);
      TimeSpan ts = new TimeSpan(23, 59, 0);
      endDate = endDate + ts;

      string sql = "select * from ( select CCH.CaseID as CaseID, CCR.CrossReferenceNumber as CrossReferenceNumber from Justice.dbo.ClkCaseHdr CCH " +
        "inner join Justice.dbo.CaseCrossReference CCR on CCR.CaseID = CCH.CaseID inner join Justice.dbo.CaseAssignHist CAH on CAH.CaseID = CCH.CaseID " +
                $" where  (CCR.CrossReferenceTypeID = 90525 or CCR.CrossReferenceTypeID = 89761) and CCH.TimestampCreate between '{startDate.Value.ToString("MM/dd/yyyy HH:mm:ss")}'" +
            $" and '{endDate.Value.ToString("MM/dd/yyyy HH:mm:ss")}'" +
            ") x where CrossReferenceNumber in (select CCR.CrossReferenceNumber as CrossReferenceNumber from(select CaseID, TimestampCreate from Justice.dbo.CaseCrossReference CCR where CCR.CrossReferenceTypeID = '1454') CCH " +
        "inner join Justice.dbo.CaseCrossReference CCR on CCR.CaseID = CCH.CaseID inner join Justice.dbo.CaseAssignHist CAH on CAH.CaseID = CCH.CaseID " +
        $" where  (CCR.CrossReferenceTypeID = 90525 or CCR.CrossReferenceTypeID = 89761) and CCH.TimestampCreate between '{startDate.Value.ToString("MM/dd/yyyy HH:mm:ss")}'" +
            $" and '{endDate.Value.ToString("MM/dd/yyyy HH:mm:ss")}'" +
        "group by CrossReferenceNumber having count(*) > 1) order by CrossReferenceNumber";
      return sql;
    }

     
    private string createSQLForReport(DateTime? startDate, DateTime? endDate)
    {
      TimeSpan ts = new TimeSpan(23, 59, 0);
      endDate = endDate + ts;
      string sql = "select CAH.CaseNbr, CCR.CrossReferenceNumber from Justice.dbo.CaseAssignHist CAH " +
                   "inner join Justice.dbo.CaseCrossReference CCR on CAH.CaseID = CCR.CaseID " +
                   "where CAH.CaseID not in (select CaseID from Justice.dbo.CaseCrossReference CCR where CCR.CrossReferenceTypeID = 1454) " +
                   $"and CAH.TimestampCreate between '{startDate.Value.ToString("MM / dd / yyyy HH: mm: ss")}'" +
                   $" and '{endDate.Value.ToString("MM/dd/yyyy HH:mm:ss")}' and CCR.CrossReferenceTypeID = '1453'";

      return sql;
    }










    private void AddInformationToJob()
    {
      int jobTaskID = 0;
      int jobProcessID = 0;

      if (Int32.TryParse(Context.taskID, out jobTaskID) && Int32.TryParse(Context.jobProcessID, out jobProcessID))
      {
        object Parms = new object[,] { { "SEVERITY" }, { "2" } };

        ITYLJobTaskUtility taskUtility = (JobProcessingInterface.ITYLJobTaskUtility)Activator.CreateInstance(Type.GetTypeFromProgID("Tyler.Odyssey.JobProcessing.TYLJobTaskUtility.cTask"));

        taskUtility.AddTextMessage(Context.SiteID, jobProcessID, jobTaskID, "The job completed successfully, but some cases were not processed. Please see the attached error file for a list of those cases and the errors associated with each. A list manager list containing the cases in error was also created.", ref Parms);
      }
    }


    private void LogErrors()
    {
      using (StreamWriter writer = GetTempFile())
      {
        Logger.WriteToLog("Beginning to write to temp file.", LogLevel.Intermediate);

        // Write the file header
        writer.WriteLine("CaseNumber,CaseID,CaseFlag,Error");

        // For each error, write some information.
        Context.Errors.ForEach((BaseCustomException f) => WriteErrorToLog(f, writer));

        Logger.WriteToLog("Finished writing to temp file.", LogLevel.Intermediate);

        AttachTempFileToJobOutput(writer, @"Add Remove Case Flags Action - Errors");
      }
    }


    private void WriteErrorToLog(BaseCustomException exception, StreamWriter writer)
    {
      writer.WriteLine(string.Format("\"{0}\"", exception.CustomMessage));
    }


    private StreamWriter GetTempFile()
    {
      if (TaskUtility == null)
        return null;

      string filePath = TaskUtility.GenerateFile(Context.SiteID, ref taskParms);
      StreamWriter fileWriter = new StreamWriter(filePath, true);

      Logger.WriteToLog("Created temp file at location: " + filePath, LogLevel.Basic);

      return fileWriter;
    }


    private void AttachTempFileToJobOutput(StreamWriter writer, string errorFileName)
    {
      Logger.WriteToLog("Begining AttachTempFileToJobOutput()", LogLevel.Intermediate);
      Logger.WriteToLog(writer == null ? "File is NULL" : "File is NOT NULL", LogLevel.Intermediate);

      if (writer != null && TaskUtility != null)
      {
        FileStream fileStream = writer.BaseStream as FileStream;
        string filePath = fileStream.Name;
        Logger.WriteToLog("File Path: " + filePath, LogLevel.Intermediate);

        writer.Close();

        if (filePath.Length > 0 && errorFileName.Length > 0)
          AttachFile(filePath, errorFileName);

        Logger.WriteToLog("Completed AttachTempFileToJobOutput()", LogLevel.Intermediate);
      }
    }


    private void AttachFile(string filepath, string filename)
    {
      DataProcessor.Logger.WriteToLog("Begin AttachFile()", Tyler.Odyssey.Utils.LogLevel.Intermediate);
      int nodeID = 0;
      int taskIDInt = 0;
      int jobProcessIDInt = 0;

      if (TaskUtility != null)
      {
        if (Int32.TryParse(Context.taskID, out taskIDInt) && Int32.TryParse(Context.jobProcessID, out jobProcessIDInt))
        {
          int documentID = TaskUtility.AddOutputDocument(this.siteKey, taskIDInt, jobProcessIDInt, -1, filepath, Context.UserID, nodeID, ref taskParms);

          if (documentID > 0)
          {
            TaskUtility.AddOutputParams(this.siteKey, taskIDInt, "TEXT", documentID, filename, TaskDocument, ref taskParms);

            TaskUtility.DeleteTempFile(filepath);

            this.OutputJobTaskXML = TaskDocument.documentElement.xml;
          }
        }
      }

      DataProcessor.Logger.WriteToLog("End Attach()", Tyler.Odyssey.Utils.LogLevel.Intermediate);
    }
  }
}