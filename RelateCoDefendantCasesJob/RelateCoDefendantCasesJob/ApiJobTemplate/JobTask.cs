using System;
using System.Runtime.InteropServices;
using Tyler.Odyssey.JobProcessing;

namespace RelateCoDefendantCasesJob
{
  [ClassInterface(ClassInterfaceType.None)]
  [Guid("f2967f41-c37e-43e6-9aa0-452b1a656f7a")]
  [ComVisible(true)]
  public class JobTask : Task
  {
    protected override void SetupProcessor(string SiteID, string JobTaskXML)
    {
      Processor = new DataProcessor(SiteID, JobTaskXML);

      ((DataProcessor)Processor).TaskParms = this.jobTaskParms;
      ((DataProcessor)Processor).TaskUtility = this.taskUtility;
      ((DataProcessor)Processor).TaskDocument = this.taskDocument;

      UserID = ((DataProcessor)Processor).Context.UserID;
    }

    private int UserID { get; set; }
  }
}
