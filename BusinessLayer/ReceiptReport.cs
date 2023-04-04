using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using CommonLayer;
using System.Data;


namespace BusinessLayer
{
   public class ReceiptReport
    {
         
       #region User Defined Objects
        DBAccess dbObj = null;
        #endregion

        #region Constructors
        public ReceiptReport()
        {
            dbObj = new DBAccess();
        }
        #endregion

        #region getReceipt byDateWise
        public DataSet getReceiptbyDateWise(string sTable, string sFmdate, string sToDate)
        {
            DataSet ds = new DataSet();
            string sQry = "select A.ReceiptNo,A.LedgerType,B.LedgerName,A.ReceiptDate,C.Payment_Mode,A.BankName,A.Chequeno,A.DaybookId ,A.Amount,A.Narration from tblReceipt_" + sTable + " A, tblLedger B,tblPaymentMode C where A.PaymodeID=C.Payment_ID and A.LedgerID=B.LedgerID  and ReceiptDate between '" + Convert.ToDateTime(sFmdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(sToDate).ToString("yyyy-MM-dd") + "' ";
            ds = dbObj.InlineExecuteDataSet(sQry);
            return ds;
        }
        #endregion
    }
}
