using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using CommonLayer;
using System.Data;


namespace BusinessLayer
{
   public class PaymentReport
    {
        #region User Defined Objects
        DBAccess dbObj = null;
        #endregion

        #region Constructors
        public PaymentReport()
        {
            dbObj = new DBAccess();
        }
        #endregion

        #region getPayment byDateWise
        public DataSet getPaymentbyDateWise(string sTable, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string sQry = "select A.PaymentID,A.LedgerType,C.Payment_Mode,A.PaymentDate,B.LedgerName,A.Amount,A.BankName,A.Chequeno,A.DaybookId,A.Narration from  tblPayment_" + sTable + " A,tblLedger B,tblPaymentMode C where A.LedgerID=B.LedgerID and A.PayModeID=C.Payment_ID and PaymentDate between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' ";
            ds = dbObj.InlineExecuteDataSet(sQry);
            return ds;
        }
        #endregion
    }
}
