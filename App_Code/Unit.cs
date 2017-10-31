using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Unit
/// </summary>
public class Unit
{
    public int unitID;
    public string unitName;
    public int chapterCount;

    public Unit(int unitID)
    {
        this.unitID = unitID;
        DataTable UnitTB = AppData.GetUnitDetail(unitID);
        if(UnitTB.Rows.Count > 0)
        {
            this.unitID = unitID;
            this.unitName = UnitTB.Rows[0]["UnitName"].ToString();
            this.chapterCount = 0;
        }

    }
}