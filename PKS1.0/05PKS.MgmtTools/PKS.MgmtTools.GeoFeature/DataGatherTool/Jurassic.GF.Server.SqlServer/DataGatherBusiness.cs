﻿using GGGXParse;
using Jurassic.GF.Interface;
using Jurassic.GF.Interface.Model;
using Jurassic.GF.Server.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Jurassic.GF.Server.SqlServer
{
    public class DataGatherBusiness : IDataGather
    {
        SubmissionResult result = null;
        SubmissionError subErr = null;
        List<SubmissionError> ListsubErr = null;
        /// <summary>
        /// 删除版本  暂未实现(需要级联删除)
        /// </summary>
        /// <param name="boid"></param>
        /// <returns></returns>
        public bool DelDataGather(string boid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断版本是否已经存在
        /// </summary>
        /// <param name="DataGather"></param>
        /// <returns></returns>
        public bool ExistDataGather(DataGatherModel DataGather)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据对象类型ID获取版本列表
        /// </summary>
        /// <param name="BOTID"></param>
        /// <returns></returns>
        public List<DataGatherModel> GetDataGatherByBOTID(string BOTID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM DATAGATHER A  WHERE A.ENVENTDATA=(SELECT MAX(ENVENTDATA) FROM DATAGATHER B  WHERE B.BOTID=@BOTID) ");
            strSql.Append(" AND A.BOTID=@BOTID ");
            SqlParameter[] parameters = {
                                new SqlParameter("BOTID", SqlDbType.VarChar,36)
                                           };
            parameters[0].Value = BOTID;
            DataTable dt = SqlServerDBHelper.GetDataTable(strSql.ToString(), parameters);
            List<DataGatherModel> list = new List<DataGatherModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    DataGatherModel model = new DataGatherModel();
                    model.BOTID = item["BOTID"].ToString();
                    model.ENVENT = item["ENVENT"].ToString();
                    model.ENVENTDATA = Convert.ToDateTime(item["ENVENTDATA"].ToString());
                    model.UPLOADDATE = Convert.ToDateTime(item["UPLOADDATE"].ToString());
                    model.GATHERER = item["GATHERER"].ToString();
                    model.GATHERID = item["GATHERID"].ToString();
                    model.NOTE = item["NOTE"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 添加版本
        /// </summary>
        /// <param name="DataGather"></param>
        /// <returns></returns>
        public bool InsertDataGather(DataGatherModel DataGather)
        {
            StringBuilder strAddSql = new StringBuilder();
            strAddSql.Append(" INSERT INTO DATAGATHER(  ");
            strAddSql.Append(" GATHERID,ENVENT,ENVENTDATA,GATHERER,UPLOADDATE,NOTE,BOTID)");
            strAddSql.Append(" VALUES (@GATHERID,@ENVENT,@ENVENTDATA,@GATHERER,@UPLOADDATE,@NOTE,@BOTID)");

            SqlParameter[] parameters = {
                                new SqlParameter("GATHERID", SqlDbType.VarChar,36),
                                new SqlParameter("ENVENT", SqlDbType.VarChar,50),
                                new SqlParameter("ENVENTDATA", SqlDbType.Date),
                                new SqlParameter("GATHERER", SqlDbType.VarChar,50),
                                new SqlParameter("UPLOADDATE", SqlDbType.Date),
                                new SqlParameter("NOTE", SqlDbType.VarChar,100),
                                new SqlParameter("BOTID", SqlDbType.VarChar,36)
                                               };
            parameters[0].Value = Guid.NewGuid().ToString();
            parameters[1].Value = DataGather.ENVENT;
            parameters[2].Value = DataGather.ENVENTDATA;
            parameters[4].Value = DataGather.GATHERER;
            parameters[5].Value = DataGather.UPLOADDATE;
            parameters[6].Value = DataGather.NOTE;
            parameters[7].Value = DataGather.BOTID;
            return SqlServerDBHelper.ExecuteCommand(strAddSql.ToString(), parameters) > 0 ? true : false;
        }

        /// <summary>
        ///数据采集
        /// </summary>
        /// <param name="SaveData"></param>
        /// <returns></returns>
        public SubmissionResult Submit(DataGatherModel dataGather, List<GeoFeature> SaveData)
        {
            int i = 0;
            int iInsertCount = 0;//记录添加条数
            int iUpdateCount = 0;//记录修改条数
            bool IsInsert = true;
            result = new SubmissionResult();
            ListsubErr = new List<SubmissionError>();
            SQLEntity sqlEntity;
            SqlParameter[] parameters;
            StringBuilder strSql = new StringBuilder();

            if (!string.IsNullOrEmpty(dataGather.GATHERID))
            {
                //1.根据版本号 找到需要修改的表   1.修改历史表数据  2.修改最新表数据
                string GeometryTable = string.Empty;
                string propertyTable = string.Empty;
                string gatherid = string.Empty;
                gatherid = SqlServerDBHelper.GetDataTable(string.Format(" SELECT GATHERID FROM GEOMETRY WHERE GATHERID='{0}'", dataGather.GATHERID)).Rows[0]["GATHERID"].ToString();
                if (!string.IsNullOrEmpty(gatherid))
                {
                    GeometryTable = "GEOMETRY";
                    propertyTable = "PROPERTY";
                }
                else
                {
                    GeometryTable = "HISGEOMETRY";
                    propertyTable = "HISPROPERTY";
                }
                //修改历史版本数据不修改版本信息，只修改版本下的数据集信息
                #region 修改历史版本数据
                foreach (GeoFeature bo in SaveData)
                {
                    subErr = new SubmissionError();
                    List<SQLEntity> sqlList = new List<SQLEntity>();
                    bool isBoExist = false;
                    BoMode seachFt = new Comm().GetBoListByName(bo.NAME, bo.FT);
                    if (seachFt != null)
                    {
                        isBoExist = true;
                        //对象存在则需要把带入库的对象id都换成库中的该对象id
                        bo.BOID = seachFt.BOID;
                    }
                    else
                    {
                        bo.BOID = Guid.NewGuid().ToString();
                    }

                    #region 添加对象
                    //存在且保留
                    if (isBoExist && bo.UNCHANGOROVERRIDE == true) ;
                    //覆盖
                    else
                    {
                        strSql = new StringBuilder();
                        //对象不存在
                        if (!isBoExist)
                        {
                            strSql.Append(" INSERT INTO BO(  ");
                            strSql.Append(" NAME,BOID,BOTID) ");
                            strSql.Append(" VALUES (@NAME,@BOID,@BOTID)");
                            IsInsert = true;
                        }
                        //对象存在
                        else
                        {
                            strSql.Append(" UPDATE BO SET ");
                            strSql.Append(" NAME=@NAME ");
                            strSql.Append(" WHERE BOID=@BOID  AND BOTID= @BOTID ");
                            IsInsert = false;
                        }
                        parameters = new SqlParameter[] {
                                     new SqlParameter("NAME", SqlDbType.VarChar,50),
                                     new SqlParameter("BOID", SqlDbType.VarChar,36),
                                     new SqlParameter("BOTID", SqlDbType.VarChar,36)
                                                         };
                        parameters[0].Value = bo.NAME;
                        parameters[1].Value = bo.BOID == null ? Guid.NewGuid().ToString() : bo.BOID;
                        //根据FT查找对象的BOTID没用找到时，添加会出现异常
                        parameters[2].Value = SqlServerDBHelper.GetDataTable(string.Format(" SELECT BOTID FROM OBJECTTYPE WHERE FT='{0}'", bo.FT)).Rows[0]["BOTID"].ToString();
                        if (parameters[2].Value == null)
                        {
                            subErr.BOName = bo.NAME;
                            subErr.Error = bo.NAME + "对应的对象类型不存在";
                            result.TotalBO = SaveData.Count;
                            result.UpdatedBO = iUpdateCount;
                            result.FailedBO = ListsubErr.Count + 1;
                            result.InsertedBO = iInsertCount;
                            result.Errors.Add(subErr);
                            continue;
                        }
                        sqlEntity = new SQLEntity();
                        sqlEntity.Sqlstr = strSql.ToString();
                        sqlEntity.SqlServerParameter = parameters;
                        sqlList.Add(sqlEntity);
                    }
                    #endregion

                    #region 添加坐标数据
                    bool isGeometryExist = false;
                    //如果对象存在，需要判断坐标是否存在，只要该对象存在一组坐标，就认为该对象坐标存在
                    if (isBoExist)
                    {
                        if (!string.IsNullOrEmpty(gatherid))
                        {
                            isGeometryExist = new GeometryBusiness().GetGeometryByID(bo.BOID).Count > 0 ? true : false;
                        }
                        else
                        {
                            isGeometryExist = new HisGeometryBusiness().GetHisGeometryByID(bo.BOID, dataGather.GATHERID).Count > 0 ? true : false;
                        }
                    }
                    //坐标且保留
                    if (isGeometryExist && bo.UNCHANGOROVERRIDE == true) ;
                    //覆盖
                    else
                    {
                        if (bo.IsImportGeometry)
                        {
                            //坐标存在，先删除
                            if (isGeometryExist)
                            {
                                strSql = new StringBuilder();
                                strSql.Append(string.Format(" DELETE FROM {0}", GeometryTable));
                                strSql.Append(" WHERE BOID =@BOID AND GATHERID=@GATHERID ");

                                parameters = new SqlParameter[] {
                                             new SqlParameter("BOID", SqlDbType.VarChar, 36),
                                             new SqlParameter("GATHERID", SqlDbType.VarChar, 36)
                                                   };
                                parameters[0].Value = bo.BOID;
                                parameters[1].Value = dataGather.GATHERID;

                                sqlEntity = new SQLEntity();
                                sqlEntity.Sqlstr = strSql.ToString();
                                sqlEntity.SqlServerParameter = parameters;
                                sqlList.Add(sqlEntity);
                            }

                            foreach (Geometry geometry in bo.GeometryList)
                            {
                                strSql = new StringBuilder();
                                strSql.Append(string.Format(" INSERT INTO {0}(  ", GeometryTable));
                                strSql.Append(" BOID,NAME,GATHERID,GEOMETRY,SOURCEDB)");
                                strSql.Append(" VALUES (@BOID,@NAME,@GATHERID,GEOGRAPHY::STGeomFromText(@GEOMETRY, 4326),@SOURCEDB)");

                                parameters = new SqlParameter[]{
                                             new SqlParameter("BOID", SqlDbType.VarChar,36),
                                             new SqlParameter("NAME", SqlDbType.VarChar,100),
                                             new SqlParameter("GATHERID", SqlDbType.VarChar,36),
                                             new SqlParameter("GEOMETRY", SqlDbType.VarChar),
                                             new SqlParameter("SOURCEDB", SqlDbType.VarChar,100)
                                           };
                                parameters[0].Value = bo.BOID == null ? System.Guid.NewGuid().ToString() : bo.BOID;
                                parameters[1].Value = geometry.NAME == null ? geometry.GEOMETRY.Split('(')[0] : geometry.NAME;
                                parameters[2].Value = dataGather.GATHERID;
                                parameters[3].Value = geometry.GEOMETRY;
                                parameters[4].Value = geometry.SOURCEDB;

                                sqlEntity = new SQLEntity();
                                sqlEntity.Sqlstr = strSql.ToString();
                                sqlEntity.SqlServerParameter = parameters;
                                sqlList.Add(sqlEntity);
                            }
                        }
                    }
                    #endregion

                    #region 添加属性数据
                    foreach (Property property in bo.PropertyList)
                    {
                        if (bo.IsImportProperty && bo.IsImportExtendProperty)
                        {
                            property.BOID = bo.BOID;
                            strSql = new StringBuilder();
                            bool isPropertyExist = false;
                            if (isBoExist)
                            {
                                if (!string.IsNullOrEmpty(gatherid))
                                {
                                    isPropertyExist = new BOPropertyBusiness().GetListByID(property.BOID, property.NS).Count > 0 ? true : false;
                                }
                                else
                                {
                                    isPropertyExist = new HisPropertyBuiness().GetHisPropertyByID(property.BOID, property.NS, dataGather.GATHERID).Count > 0 ? true : false;
                                }

                            }
                            //该条属性存在且保留，跳过
                            if (isPropertyExist && bo.UNCHANGOROVERRIDE == true) ;
                            else
                            {
                                if (!isPropertyExist)
                                {
                                    strSql.Append(string.Format(" INSERT INTO {0}(  ", propertyTable));
                                    strSql.Append(" GATHERID,MD,MDSOURCE,BOID,NS) ");
                                    strSql.Append(" VALUES (@GATHERID,@MD,@MDSOURCE,@BOID,@NS) ");
                                }
                                else
                                {
                                    strSql.Append(string.Format(" UPDATE {0} SET ", propertyTable));
                                    strSql.Append(" GATHERID=@GATHERID, MD=@MD ,MDSOURCE=@MDSOURCE ");
                                    strSql.Append(" WHERE BOID=@BOID AND NS=@NS");
                                }

                                parameters = new SqlParameter[] {
                                             new SqlParameter("GATHERID", SqlDbType.VarChar,36),
                                             new SqlParameter("MD", SqlDbType.Xml),
                                             new SqlParameter("MDSOURCE", SqlDbType.VarChar,50),
                                             new SqlParameter("BOID", SqlDbType.VarChar,36),
                                             new SqlParameter("NS", SqlDbType.VarChar,50)
                                                        };
                                parameters[0].Value = dataGather.GATHERID;
                                parameters[1].Value = property.MD;
                                parameters[2].Value = property.MdSource;
                                parameters[3].Value = bo.BOID;
                                parameters[4].Value = property.NS;

                                sqlEntity = new SQLEntity();
                                sqlEntity.Sqlstr = strSql.ToString();
                                sqlEntity.SqlServerParameter = parameters;
                                sqlList.Add(sqlEntity);
                            }
                        }
                        else
                        {
                            if (bo.IsImportProperty)
                            {
                                var SeachProperty = bo.PropertyList.Find(p => p.ISUSERDEFIN == "1");
                                SeachProperty.BOID = bo.BOID;
                                strSql = new StringBuilder();
                                bool isPropertyExist = false;
                                if (isBoExist)
                                {
                                    if (!string.IsNullOrEmpty(gatherid))
                                    {
                                        isPropertyExist = new BOPropertyBusiness().GetListByID(SeachProperty.BOID, SeachProperty.NS).Count > 0 ? true : false;
                                    }
                                    else
                                    {
                                        isPropertyExist = new HisPropertyBuiness().GetHisPropertyByID(property.BOID, property.NS, dataGather.GATHERID).Count > 0 ? true : false;
                                    }

                                }
                                //该条属性存在且保留，跳过
                                if (isPropertyExist && bo.UNCHANGOROVERRIDE == true) ;
                                else
                                {
                                    if (!isPropertyExist)
                                    {
                                        strSql.Append(string.Format(" INSERT INTO {0}(  ", propertyTable));
                                        strSql.Append(" GATHERID,MD,MDSOURCE,BOID,NS) ");
                                        strSql.Append(" VALUES (@GATHERID,@MD,@MDSOURCE,@BOID,@NS) ");
                                    }
                                    else
                                    {
                                        strSql.Append(string.Format(" UPDATE {0} SET ", propertyTable));
                                        strSql.Append(" GATHERID=@GATHERID, MD=@MD ,MDSOURCE=@MDSOURCE ");
                                        strSql.Append(" WHERE BOID=@BOID AND NS=@NS");
                                    }

                                    parameters = new SqlParameter[] {
                                                 new SqlParameter("GATHERID", SqlDbType.VarChar,36),
                                                 new SqlParameter("MD", SqlDbType.Xml),
                                                 new SqlParameter("MDSOURCE", SqlDbType.VarChar,50),
                                                 new SqlParameter("BOID", SqlDbType.VarChar,36),
                                                 new SqlParameter("NS", SqlDbType.VarChar,50)
                                                        };
                                    parameters[0].Value = dataGather.GATHERID;
                                    parameters[1].Value = property.MD;
                                    parameters[2].Value = property.MdSource;
                                    parameters[3].Value = bo.BOID;
                                    parameters[4].Value = property.NS;

                                    sqlEntity = new SQLEntity();
                                    sqlEntity.Sqlstr = strSql.ToString();
                                    sqlEntity.SqlServerParameter = parameters;
                                    sqlList.Add(sqlEntity);
                                }
                            }
                            else
                            {
                                var SeachProperty = bo.PropertyList.Find(p => p.ISUSERDEFIN == "0");
                                SeachProperty.BOID = bo.BOID;
                                strSql = new StringBuilder();
                                bool isPropertyExist = false;
                                if (isBoExist)
                                {
                                    if (!string.IsNullOrEmpty(gatherid))
                                    {
                                        isPropertyExist = new BOPropertyBusiness().GetListByID(SeachProperty.BOID, SeachProperty.NS).Count > 0 ? true : false;
                                    }
                                    else
                                    {
                                        isPropertyExist = new HisPropertyBuiness().GetHisPropertyByID(property.BOID, property.NS, dataGather.GATHERID).Count > 0 ? true : false;
                                    }

                                }
                                //该条属性存在且保留，跳过
                                if (isPropertyExist && bo.UNCHANGOROVERRIDE == true) ;
                                else
                                {
                                    if (!isPropertyExist)
                                    {
                                        strSql.Append(string.Format(" INSERT INTO {0}(  ", propertyTable));
                                        strSql.Append(" GATHERID,MD,MDSOURCE,BOID,NS) ");
                                        strSql.Append(" VALUES (@GATHERID,@MD,@MDSOURCE,@BOID,@NS) ");
                                    }
                                    else
                                    {
                                        strSql.Append(string.Format(" UPDATE {0} SET ", propertyTable));
                                        strSql.Append(" GATHERID=@GATHERID, MD=@MD ,MDSOURCE=@MDSOURCE ");
                                        strSql.Append(" WHERE BOID=@BOID AND NS=@NS");
                                    }

                                    parameters = new SqlParameter[] {
                                                 new SqlParameter("GATHERID", SqlDbType.VarChar,36),
                                                 new SqlParameter("MD", SqlDbType.Xml),
                                                 new SqlParameter("MDSOURCE", SqlDbType.VarChar,50),
                                                 new SqlParameter("BOID", SqlDbType.VarChar,36),
                                                 new SqlParameter("NS", SqlDbType.VarChar,50)
                                                        };
                                    parameters[0].Value = dataGather.GATHERID;
                                    parameters[1].Value = property.MD;
                                    parameters[2].Value = property.MdSource;
                                    parameters[3].Value = bo.BOID;
                                    parameters[4].Value = property.NS;

                                    sqlEntity = new SQLEntity();
                                    sqlEntity.Sqlstr = strSql.ToString();
                                    sqlEntity.SqlServerParameter = parameters;
                                    sqlList.Add(sqlEntity);
                                }
                            }
                        }
                    }
                    #endregion

                    try
                    {
                        //存在需要执行的sql语句
                        if (sqlList.Count > 0)
                        {
                            i = i + (SqlServerDBHelper.ExecuteSqlTran(sqlList) == true ? 1 : 0);
                            if (IsInsert == true)
                            {
                                iInsertCount++;
                            }
                            else
                            {
                                iUpdateCount++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //记录入库错误信息
                        subErr.BOName = bo.NAME;
                        subErr.Error = "空间数据有误！";
                        result.Errors.Add(subErr);
                        //continue;
                        //记录完毕
                        throw ex;
                    }

                    //构造需要返回的SubmissionResult
                    result.TotalBO = SaveData.Count;
                    result.UpdatedBO = iUpdateCount;
                    result.FailedBO = ListsubErr.Count;
                    result.InsertedBO = iInsertCount;
                    //result.Errors.AddRange(ListsubErr);
                    //结束
                }
                #endregion
            }
            else
            {
                #region 新增版本
                dataGather.GATHERID = Guid.NewGuid().ToString();
                List<SQLEntity> sqlList = new List<SQLEntity>();
                //新增版本信息
                strSql = new StringBuilder();
                strSql.Append(" INSERT INTO DATAGATHER(  ");
                strSql.Append(" GATHERID,ENVENT,ENVENTDATA,GATHERER,UPLOADDATE,NOTE,BOTID)");
                strSql.Append(" VALUES (@GATHERID,@ENVENT,@ENVENTDATA,@GATHERER,@UPLOADDATE,@NOTE,@BOTID)");

                parameters = new SqlParameter[] {
                             new SqlParameter("GATHERID", SqlDbType.VarChar,36),
                             new SqlParameter("ENVENT", SqlDbType.VarChar,50),
                             new SqlParameter("ENVENTDATA", SqlDbType.Date),
                             new SqlParameter("GATHERER", SqlDbType.VarChar,50),
                             new SqlParameter("UPLOADDATE", SqlDbType.Date),
                             new SqlParameter("NOTE", SqlDbType.VarChar,100),
                             new SqlParameter("BOTID", SqlDbType.VarChar,36)
                                         };
                parameters[0].Value = dataGather.GATHERID;
                parameters[1].Value = dataGather.ENVENT;
                parameters[2].Value = dataGather.ENVENTDATA;
                parameters[3].Value = dataGather.GATHERER;
                parameters[4].Value = dataGather.UPLOADDATE;
                parameters[5].Value = dataGather.NOTE;
                parameters[6].Value = dataGather.BOTID;
                sqlEntity = new SQLEntity();
                sqlEntity.Sqlstr = strSql.ToString();
                sqlEntity.SqlServerParameter = parameters;
                sqlList.Add(sqlEntity);

                //添加历史版本记录与   最新版本记录   增量备份
                //查询最近的上一个版本
                DataTable model = SqlServerDBHelper.GetDataTable(string.Format("SELECT * FROM DATAGATHER A  WHERE A.ENVENTDATA=(SELECT MAX(ENVENTDATA) FROM DATAGATHER B )  AND A.BOTID='{0}' ", dataGather.BOTID));
                if (model != null && model.Rows.Count > 0)
                {
                    string names = string.Empty;
                    //存在上个版本  当前数据和和上一个版本对比 增量备份
                    for (int j = 0; j < SaveData.Count(); j++)
                    {
                        if (j == SaveData.Count() - 1)
                        {
                            names += "'" + SaveData[j].NAME.Trim() + "'";
                        }
                        else
                        {
                            names += "'" + SaveData[j].NAME.Trim() + "',";
                        }
                    }
                    #region 备份空间和属性数据 
                    DataTable backupgeometry = SqlServerDBHelper.GetDataTable(string.Format(" SELECT BOID,NAME,GATHERID, t.GEOMETRY.STAsText() GEOMETRY,T.SOURCEDB FROM GEOMETRY T WHERE T.GATHERID='{0}' ", model.Rows[0]["GATHERID"].ToString()));
                    foreach (DataRow item in backupgeometry.Rows)
                    {
                        //插入到历史表中
                        strSql = new StringBuilder();
                        strSql.Append(" INSERT INTO HISGEOMETRY(  ");
                        strSql.Append(" BOID,NAME,GATHERID,GEOMETRY,SOURCEDB)");
                        strSql.Append(" VALUES (@BOID,@NAME,@GATHERID,GEOMETRY::STGeomFromText(@GEOMETRY, 4326),@SOURCEDB)");

                        parameters = new SqlParameter[] {
                            new SqlParameter("BOID", SqlDbType.VarChar,36),
                            new SqlParameter("NAME", SqlDbType.VarChar,50),
                            new SqlParameter("GATHERID", SqlDbType.VarChar,36),
                            new SqlParameter("GEOMETRY", SqlDbType.VarChar),
                            new SqlParameter("SOURCEDB", SqlDbType.VarChar,50)
                                           };
                        parameters[0].Value = item["BOID"].ToString();
                        parameters[1].Value = item["NAME"].ToString();
                        parameters[2].Value = item["GATHERID"].ToString();
                        parameters[3].Value = item["GEOMETRY"].ToString();
                        parameters[4].Value = item["SOURCEDB"].ToString();
                        sqlEntity = new SQLEntity();
                        sqlEntity.Sqlstr = strSql.ToString();
                        sqlEntity.SqlServerParameter = parameters;
                        sqlList.Add(sqlEntity);
                    }
                    //需要备份的属性数据信息
                    DataTable backupproperty = SqlServerDBHelper.GetDataTable(string.Format(" SELECT T.*,T1.NAME FROM PROPERTY T LEFT JOIN BO T1  ON T.BOID=T1.BOID WHERE  T.GATHERID='{0}' ", model.Rows[0]["GATHERID"].ToString()));
                    foreach (DataRow item in backupproperty.Rows)
                    {
                        //插入到历史表中
                        strSql = new StringBuilder();
                        strSql.Append(" INSERT INTO HISPROPERTY(  ");
                        strSql.Append(" BOID,GATHERID,NS,MD,MDSOURCE)");
                        strSql.Append(" VALUES (@BOID,@GATHERID,@NS,@MD,@MDSOURCE)");

                        parameters = new SqlParameter[]{
                            new SqlParameter("BOID", SqlDbType.VarChar,36),
                            new SqlParameter("GATHERID", SqlDbType.VarChar,50),
                            new SqlParameter("NS", SqlDbType.VarChar,50),
                            new SqlParameter("MD", SqlDbType.Xml),
                            new SqlParameter("MDSOURCE", SqlDbType.VarChar,50)
                            };
                        parameters[0].Value = item["BOID"].ToString();
                        parameters[1].Value = item["GATHERID"].ToString();
                        parameters[2].Value = item["NS"].ToString();
                        parameters[3].Value = item["MD"].ToString();
                        parameters[4].Value = item["MDSOURCE"].ToString();
                        sqlEntity = new SQLEntity();
                        sqlEntity.Sqlstr = strSql.ToString();
                        sqlEntity.SqlServerParameter = parameters;
                        sqlList.Add(sqlEntity);
                    }

                    #endregion

                    #region 新版本的构建  (增量加当前数据够成新版本)
                    DataTable geometryList = SqlServerDBHelper.GetDataTable(string.Format(" SELECT T.*,T1.NAME FROM HISGEOMETRY T LEFT JOIN BO T1  ON T.BOID=T1.BOID WHERE T1.NAME NOT IN({0}) AND T.GATHERID='{1}' ", names, model.Rows[0]["GATHERID"].ToString()));
                    //需要增量备份的属性数据信息
                    DataTable propertyList = SqlServerDBHelper.GetDataTable(string.Format(" SELECT T.*,T1.NAME FROM HISPROPERTY T LEFT JOIN BO T1  ON T.BOID=T1.BOID WHERE T1.NAME NOT IN({0}) AND T.GATHERID='{1}' ", names, model.Rows[0]["GATHERID"].ToString()));
                    //与当前数据形成新版本
                    foreach (DataRow item in geometryList.Rows)
                    {
                        strSql = new StringBuilder();
                        strSql.Append(" INSERT INTO GEOMETRY(  ");
                        strSql.Append(" BOID,NAME,GATHERID,GEOMETRY,SOURCEDB)");
                        strSql.Append(" VALUES (@BOID,@NAME,GATHERID,GEOMETRY::STGEOMFROMTEXT(@GEOMETRY, 4326),@SOURCEDB)");

                        parameters = new SqlParameter[] {
                            new SqlParameter("BOID", SqlDbType.VarChar,36),
                            new SqlParameter("NAME", SqlDbType.VarChar,50),
                            new SqlParameter("GATHERID", SqlDbType.VarChar,36),
                            new SqlParameter("GEOMETRY", SqlDbType.Binary),
                            new SqlParameter("SOURCEDB", SqlDbType.VarChar,50)
                                           };
                        parameters[0].Value = item["BOID"].ToString();
                        parameters[1].Value = item["NAME"].ToString();
                        parameters[2].Value = item["GATHERID"].ToString();
                        parameters[3].Value = item["SOURCEDB"].ToString();
                        sqlEntity = new SQLEntity();
                        sqlEntity.Sqlstr = strSql.ToString();
                        sqlEntity.SqlServerParameter = parameters;
                        sqlList.Add(sqlEntity);
                    }
                    //与当前数据形成新版本
                    foreach (DataRow item in propertyList.Rows)
                    {
                        StringBuilder strInsertSql = new StringBuilder();
                        strInsertSql.Append(" INSERT INTO PROPERTY(  ");
                        strInsertSql.Append(" BOID,GATHERID,NS,MD,MDSOURCE)");
                        strInsertSql.Append(" VALUES (@BOID,@GATHERID,@NS,@MD,@MDSOURCE)");

                        parameters = new SqlParameter[] {
                            new SqlParameter("BOID", SqlDbType.VarChar,36),
                            new SqlParameter("GATHERID", SqlDbType.VarChar,50),
                            new SqlParameter("NS", SqlDbType.VarChar,50),
                            new SqlParameter("MD", SqlDbType.Xml),
                            new SqlParameter("MDSOURCE", SqlDbType.VarChar,50)
                            };
                        parameters[0].Value = item["BOID"].ToString();
                        parameters[1].Value = item["GATHERID"].ToString();
                        parameters[2].Value = item["NS"].ToString();
                        parameters[2].Value = item["MD"].ToString();
                        parameters[4].Value = item["MDSOURCE"];
                        sqlEntity = new SQLEntity();
                        sqlEntity.Sqlstr = strInsertSql.ToString();
                        sqlEntity.SqlServerParameter = parameters;
                        sqlList.Add(sqlEntity);
                    }
                    //删除新版本之前的数据

                    strSql = new StringBuilder();
                    strSql.Append(" DELETE  GEOMETRY  ");
                    strSql.Append(" WHERE GATHERID =@GATHERID");
                    parameters = new SqlParameter[] {
                            new SqlParameter("GATHERID",SqlDbType.VarChar,36)
                            };
                    parameters[0].Value = model.Rows[0]["GATHERID"].ToString();
                    sqlEntity = new SQLEntity();
                    sqlEntity.Sqlstr = strSql.ToString();
                    sqlEntity.SqlServerParameter = parameters;
                    sqlList.Add(sqlEntity);
                    //需要增量备份的属性数据信息
                    strSql = new StringBuilder();
                    strSql.Append(" DELETE  PROPERTY  ");
                    strSql.Append(" WHERE GATHERID =@GATHERID");
                    parameters = new SqlParameter[] {
                            new SqlParameter("GATHERID",SqlDbType.VarChar,36)
                            };
                    parameters[0].Value = model.Rows[0]["GATHERID"].ToString();
                    sqlEntity = new SQLEntity();
                    sqlEntity.Sqlstr = strSql.ToString();
                    sqlEntity.SqlServerParameter = parameters;
                    sqlList.Add(sqlEntity);
                    #endregion
                }
                #region 添加新数据
                foreach (GeoFeature bo in SaveData)
                {
                    subErr = new SubmissionError();
                    bool isBoExist = false;
                    BoMode seachFt = new Comm().GetBoListByName(bo.NAME, bo.FT);
                    if (seachFt != null && !string.IsNullOrEmpty(seachFt.BOID))
                    {
                        isBoExist = true;
                        //对象存在则需要把带入库的对象id都换成库中的该对象id
                        bo.BOID = seachFt.BOID;
                    }
                    else
                    {
                        bo.BOID = Guid.NewGuid().ToString();
                    }

                    #region 添加对象
                    //存在且保留
                    if (isBoExist && bo.UNCHANGOROVERRIDE == true) ;
                    //覆盖
                    else
                    {
                        strSql = new StringBuilder();
                        //对象不存在
                        if (!isBoExist)
                        {
                            strSql.Append(" INSERT INTO BO(  ");
                            strSql.Append(" NAME,ISUSE,BOID,BOTID) ");
                            strSql.Append(" VALUES (@NAME,@ISUSE,@BOID,@BOTID)");
                            IsInsert = true;
                        }
                        //对象存在
                        else
                        {
                            strSql.Append(" UPDATE BO SET ");
                            strSql.Append(" NAME=@NAME ,ISUSE=@ISUSE");
                            strSql.Append(" WHERE BOID=@BOID  AND BOTID= @BOTID ");
                            IsInsert = false;
                        }
                        parameters = new SqlParameter[] {
                                 new SqlParameter("NAME", SqlDbType.VarChar,50),
                                 new SqlParameter("ISUSE", SqlDbType.Char,1),
                                 new SqlParameter("BOID", SqlDbType.VarChar,36),
                                 new SqlParameter("BOTID", SqlDbType.VarChar,36)
                                                        };
                        parameters[0].Value = bo.NAME;
                        parameters[1].Value = "1";
                        parameters[2].Value = bo.BOID;
                        //根据FT查找对象的BOTID没用找到时，添加会出现异常
                        parameters[3].Value = SqlServerDBHelper.GetDataTable(string.Format(" SELECT BOTID FROM OBJECTTYPE WHERE FT='{0}'", bo.FT)).Rows[0]["BOTID"].ToString();
                        if (parameters[2].Value == null)
                        {
                            subErr.BOName = bo.NAME;
                            subErr.Error = bo.NAME + "对应的对象类型不存在";
                            result.TotalBO = SaveData.Count;
                            result.UpdatedBO = iUpdateCount;
                            result.FailedBO = ListsubErr.Count + 1;
                            result.InsertedBO = iInsertCount;
                            result.Errors.Add(subErr);
                            continue;
                        }
                        sqlEntity = new SQLEntity();
                        sqlEntity.Sqlstr = strSql.ToString();
                        sqlEntity.SqlServerParameter = parameters;
                        sqlList.Add(sqlEntity);
                    }
                    #endregion

                    //处理 是否添加空间坐标
                    #region 添加坐标数据
                    if (bo.IsImportGeometry)
                    {
                        bool isGeometryExist = new GeometryBusiness().GetGeometryByID(bo.BOID).Count > 0 ? true : false;

                        //坐标且保留
                        if (isGeometryExist && bo.UNCHANGOROVERRIDE == true) ;
                        //覆盖
                        else
                        {
                            //坐标存在，先删除
                            if (isGeometryExist)
                            {

                                strSql = new StringBuilder();
                                strSql.Append(" DELETE FROM  GEOMETRY  ");
                                strSql.Append(" WHERE BOID =@BOID ");

                                parameters = new SqlParameter[] {
                         new SqlParameter("BOID", SqlDbType.VarChar, 36)
                                   };
                                parameters[0].Value = bo.BOID;

                                sqlEntity = new SQLEntity();
                                sqlEntity.Sqlstr = strSql.ToString();
                                sqlEntity.SqlServerParameter = parameters;
                                sqlList.Add(sqlEntity);
                            }

                            foreach (Geometry geometry in bo.GeometryList)
                            {
                                strSql = new StringBuilder();
                                strSql.Append(" INSERT INTO GEOMETRY(  ");
                                strSql.Append(" BOID,NAME,GATHERID,GEOMETRY,SOURCEDB)");
                                strSql.Append(" VALUES (@BOID,@NAME,@GATHERID,GEOGRAPHY::STGeomFromText(@GEOMETRY, 4326),@SOURCEDB)");

                                parameters = new SqlParameter[]{
                                             new SqlParameter("BOID", SqlDbType.VarChar,36),
                                             new SqlParameter("NAME", SqlDbType.VarChar,100),
                                             new SqlParameter("GATHERID", SqlDbType.VarChar,36),
                                             new SqlParameter("GEOMETRY", SqlDbType.VarChar),
                                             new SqlParameter("SOURCEDB", SqlDbType.VarChar,100)
                                           };
                                parameters[0].Value = bo.BOID;
                                parameters[1].Value = geometry.NAME == null ? geometry.GEOMETRY.Split('(')[0] : geometry.NAME;
                                parameters[2].Value = dataGather.GATHERID;
                                parameters[3].Value = geometry.GEOMETRY;
                                parameters[4].Value = geometry.SOURCEDB;

                                sqlEntity = new SQLEntity();
                                sqlEntity.Sqlstr = strSql.ToString();
                                sqlEntity.SqlServerParameter = parameters;
                                sqlList.Add(sqlEntity);
                            }
                        }
                    }
                    #endregion

                    #region 添加属性数据

                    foreach (Property property in bo.PropertyList)
                    {
                        if (bo.IsImportProperty && bo.IsImportExtendProperty)
                        {
                            strSql = new StringBuilder();
                            bool isPropertyExist = false;
                            if (isBoExist)
                            {
                                property.BOID = bo.BOID;
                                isPropertyExist = new BOPropertyBusiness().GetListByID(property.BOID, property.NS).Count > 0 ? true : false;
                            }
                            //该条属性存在且保留，跳过
                            if (isPropertyExist && bo.UNCHANGOROVERRIDE == true) ;
                            else
                            {
                                //不存在新增
                                if (!isPropertyExist)
                                {
                                    strSql.Append(" INSERT INTO PROPERTY(  ");
                                    strSql.Append(" MD,MDSOURCE,GATHERID,BOID,NS)");
                                    strSql.Append(" VALUES (@MD,@MDSOURCE,@GATHERID,@BOID,@NS)");
                                }
                                else//存在修改
                                {
                                    strSql.Append(" UPDATE PROPERTY SET ");
                                    strSql.Append(" MD=@MD ,MDSOURCE=@MDSOURCE,GATHERID=@GATHERID ");
                                    strSql.Append(" WHERE BOID=@BOID AND NS=@NS");
                                }

                                parameters = new SqlParameter[] {
                                             new SqlParameter("MD", SqlDbType.Xml),
                                             new SqlParameter("MDSOURCE", SqlDbType.VarChar,50),
                                             new SqlParameter("GATHERID", SqlDbType.VarChar,36),
                                             new SqlParameter("BOID", SqlDbType.VarChar,36),
                                             new SqlParameter("NS", SqlDbType.VarChar,50)
                                                        };
                                parameters[0].Value = property.MD;
                                parameters[1].Value = property.MdSource;
                                parameters[2].Value = dataGather.GATHERID;
                                parameters[3].Value = bo.BOID;
                                parameters[4].Value = property.NS;

                                sqlEntity = new SQLEntity();
                                sqlEntity.Sqlstr = strSql.ToString();
                                sqlEntity.SqlServerParameter = parameters;
                                sqlList.Add(sqlEntity);
                            }
                        }
                        else//只添加主参数
                        {
                            if (bo.IsImportProperty)
                            {
                                var SeachProperty = bo.PropertyList.Find(p => p.ISUSERDEFIN == "0");
                                strSql = new StringBuilder();
                                bool isPropertyExist = false;
                                if (isBoExist)
                                {
                                    SeachProperty.BOID = bo.BOID;
                                    isPropertyExist = new BOPropertyBusiness().GetListByID(SeachProperty.BOID, SeachProperty.NS).Count > 0 ? true : false;
                                }
                                //该条属性存在且保留，跳过
                                if (isPropertyExist && bo.UNCHANGOROVERRIDE == true) ;
                                else
                                {
                                    //不存在新增
                                    if (!isPropertyExist)
                                    {
                                        strSql.Append(" INSERT INTO PROPERTY(  ");
                                        strSql.Append(" MD,MDSOURCE,GATHERID,BOID,NS)");
                                        strSql.Append(" VALUES (@MD,@MDSOURCE,@GATHERID,@BOID,@NS)");
                                    }
                                    else//存在修改
                                    {
                                        strSql.Append(" UPDATE PROPERTY SET ");
                                        strSql.Append(" MD=@MD ,MDSOURCE=@MDSOURCE,GATHERID=@GATHERID ");
                                        strSql.Append(" WHERE BOID=@BOID AND NS=@NS");
                                    }

                                    parameters = new SqlParameter[] {
                                                 new SqlParameter("MD", SqlDbType.Xml),
                                                 new SqlParameter("MDSOURCE", SqlDbType.VarChar,50),
                                                 new SqlParameter("GATHERID", SqlDbType.VarChar,36),
                                                 new SqlParameter("BOID", SqlDbType.VarChar,36),
                                                 new SqlParameter("NS", SqlDbType.VarChar,50)
                                                        };
                                    parameters[0].Value = property.MD;
                                    parameters[1].Value = property.MdSource;
                                    parameters[2].Value = dataGather.GATHERID;
                                    parameters[3].Value = bo.BOID;
                                    parameters[4].Value = property.NS;

                                    sqlEntity = new SQLEntity();
                                    sqlEntity.Sqlstr = strSql.ToString();
                                    sqlEntity.SqlServerParameter = parameters;
                                    sqlList.Add(sqlEntity);
                                }
                            }
                            else//只添加扩展主参数
                            {
                                strSql = new StringBuilder();
                                var SeachProperty = bo.PropertyList.Find(p => p.ISUSERDEFIN == "0");
                                bool isPropertyExist = false;
                                if (isBoExist)
                                {
                                    property.BOID = bo.BOID;
                                    isPropertyExist = new BOPropertyBusiness().GetListByID(SeachProperty.BOID, SeachProperty.NS).Count > 0 ? true : false;
                                }
                                //该条属性存在且保留，跳过
                                if (isPropertyExist && bo.UNCHANGOROVERRIDE == true) ;
                                else
                                {
                                    //不存在新增
                                    if (!isPropertyExist)
                                    {
                                        strSql.Append(" INSERT INTO PROPERTY(  ");
                                        strSql.Append(" MD,MDSOURCE,GATHERID,BOID,NS)");
                                        strSql.Append(" VALUES (@MD,@MDSOURCE,@GATHERID,@BOID,@NS)");
                                    }
                                    else//存在修改
                                    {
                                        strSql.Append(" UPDATE PROPERTY SET ");
                                        strSql.Append(" MD=@MD ,MDSOURCE=@MDSOURCE,GATHERID=@GATHERID ");
                                        strSql.Append(" WHERE BOID=@BOID AND NS=@NS");
                                    }

                                    parameters = new SqlParameter[] {
                                                 new SqlParameter("MD", SqlDbType.Xml),
                                                 new SqlParameter("MDSOURCE", SqlDbType.VarChar,50),
                                                 new SqlParameter("GATHERID", SqlDbType.VarChar,36),
                                                 new SqlParameter("BOID", SqlDbType.VarChar,36),
                                                 new SqlParameter("NS", SqlDbType.VarChar,50)
                                                        };
                                    parameters[0].Value = property.MD;
                                    parameters[1].Value = property.MdSource;
                                    parameters[2].Value = dataGather.GATHERID;
                                    parameters[3].Value = bo.BOID;
                                    parameters[4].Value = property.NS;

                                    sqlEntity = new SQLEntity();
                                    sqlEntity.Sqlstr = strSql.ToString();
                                    sqlEntity.SqlServerParameter = parameters;
                                    sqlList.Add(sqlEntity);
                                }
                            }
                        }
                    }
                    #endregion
                    try
                    {
                        //存在需要执行的sql语句
                        if (sqlList.Count > 0)
                        {
                            i = i + (SqlServerDBHelper.ExecuteSqlTran(sqlList) == true ? 1 : 0);
                            if (IsInsert == true)
                            {
                                iInsertCount++;
                            }
                            else
                            {
                                iUpdateCount++;
                            }
                            sqlList = new List<SQLEntity>();
                        }
                    }
                    catch (Exception ex)
                    {
                        //记录入库错误信息
                        subErr.BOName = bo.NAME;
                        subErr.Error = "空间数据有误！";
                        result.Errors.Add(subErr);
                        continue;
                        //记录完毕
                        throw ex;
                    }
                    result.TotalBO = SaveData.Count;
                    result.UpdatedBO = iUpdateCount;
                    result.FailedBO = ListsubErr.Count;
                    result.InsertedBO = iInsertCount;
                }
                #endregion
                #endregion
            }
            return result;
        }

        /// <summary>
        /// 修改版本
        /// </summary>
        /// <param name="DataGather"></param>
        /// <returns></returns>
        public bool UpdateDataGather(DataGatherModel DataGather)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据对象类型ID对象信息
        /// </summary>
        /// <param name="botid"></param>
        /// <returns></returns>
        private List<GeoFeature> GetFeatureByBOTID(string botid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT T.BOID,T.NAME,T1.BOT,T1.FT FROM BO T ");
            strSql.Append(" LEFT JOIN OBJECTTYPE T1 ");
            strSql.Append(" ON T.BOTID=T1.BOTID ");
            strSql.Append(" WHERE T.BOTID =@BOTID ");
            SqlParameter[] parameters = {
                                new SqlParameter("BOTID", SqlDbType.VarChar,36)
                                           };
            parameters[0].Value = botid;
            DataTable dt = SqlServerDBHelper.GetDataTable(strSql.ToString(), parameters);
            List<GeoFeature> ftList = new List<GeoFeature>();
            foreach (DataRow row in dt.Rows)
            {
                GeoFeature ft = new GeoFeature();
                ft.BOID = row["BOID"].ToString();
                ft.BOT = row["BOT"].ToString();
                ft.FT = row["FT"].ToString();
                ft.NAME = row["NAME"].ToString();
                ft.PropertyList = Comm.GetPropertyByBoid(ft.BOID);
                ft.GeometryList = Comm.GetGeometryByBoid(ft.BOID);
                ftList.Add(ft);
            }
            return ftList;
        }
    }
}
