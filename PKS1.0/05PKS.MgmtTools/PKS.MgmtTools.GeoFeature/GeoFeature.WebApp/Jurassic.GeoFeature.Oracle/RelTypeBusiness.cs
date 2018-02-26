﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jurassic.GeoFeature.IDAL;
using Jurassic.GeoFeature.Model;
using Oracle.ManagedDataAccess.Client;
using System.Data;
namespace Jurassic.GeoFeature.Oracle
{
    public class RelTypeBusiness : IRelType
    {
        public bool Exist(RelTypeModel reltype)
        {
            return false;
        }
        /// <summary>
        /// 判断对象关系类型是否存在
        /// </summary>
        /// <param name="reltype"></param>
        /// <returns></returns>
        public bool Exist(RelTypeModel reltype, ref string RTID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM RELTYPE T ");
            strSql.Append(" WHERE (REMARK=:REMARK) or (BOTID1=:BOTID1 and BOTID2=:BOTID2)");
            OracleParameter[] parameters = {
                              new OracleParameter("REMARK",OracleDbType.Varchar2,50),
                              new OracleParameter("BOTID1",OracleDbType.Varchar2,36),
                              new OracleParameter("BOTID2",OracleDbType.Varchar2,36)
                                           };
            parameters[0].Value = reltype.Title;
            parameters[1].Value = reltype.Botid1;
            parameters[2].Value = reltype.Botid2;
            RelTypeModel model = DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText<RelTypeModel>(strSql.ToString(), parameters).FirstOrDefault();
            RTID = model == null ? System.Guid.NewGuid().ToString() : model.Rtid;
            return model == null ? false : true;
        }
        /// <summary>
        /// 添加对象关系类型
        /// </summary>
        /// <param name="reltype"></param>
        /// <returns></returns>
        public int Insert(RelTypeModel reltype)
        {
            StringBuilder strInsertSql = new StringBuilder();
            strInsertSql.Append(" INSERT INTO RELTYPE(  ");
            strInsertSql.Append(" RT,BOT1,BOT2) ");
            strInsertSql.Append(" VALUES (:RT,:REMARK,:BOT1,:BOT2 )");

            OracleParameter[] parameters = {
                            new OracleParameter("RT", OracleDbType.Varchar2,36),     
                            new OracleParameter("REMARK", OracleDbType.Varchar2,100),     
                            new OracleParameter("BOT1", OracleDbType.Varchar2,36),
                            new OracleParameter("BOT2", OracleDbType.Varchar2,36)
                            };
            parameters[0].Value = reltype.RT;
            parameters[1].Value = reltype.Title;
            parameters[2].Value = reltype.Bot1;
            parameters[3].Value = reltype.Bot2;

            return DBUtility.OracleDBHelper.OracleHelper.ExecuteSql(strInsertSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新对象关系类型
        /// </summary>
        /// <param name="reltype"></param>
        /// <returns></returns>
        public int Update(RelTypeModel reltype)
        {
            StringBuilder strUpdateSql = new StringBuilder();
            strUpdateSql.Append(" UPDATE RELTYPE SET ");
            strUpdateSql.Append(" TITLE=:TITLE,BOT1=:BOT1,BOT2=:BOT2");
            strUpdateSql.Append(" WHERE RT=:RT");

            OracleParameter[] parameters = {
                            new OracleParameter("TITLE", OracleDbType.Varchar2,100),
                            new OracleParameter("BOT1", OracleDbType.Varchar2,36),
                            new OracleParameter("BOT2", OracleDbType.Varchar2,36),
                            new OracleParameter("RT", OracleDbType.Varchar2,36)
                                            };
            parameters[0].Value = reltype.Title;
            parameters[1].Value = reltype.Bot1;
            parameters[2].Value = reltype.Bot2;
            parameters[3].Value = reltype.RT;
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteSql(strUpdateSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除对象关系类型
        /// </summary>
        /// <param name="reltype"></param>
        /// <returns></returns>
        public int Delete(RelTypeModel reltype)
        {
            List<Jurassic.GeoFeature.DBUtility.OracleDBHelper.SQLEntity> sqlList = new List<Jurassic.GeoFeature.DBUtility.OracleDBHelper.SQLEntity>();
            StringBuilder DelRelationSql = new StringBuilder();
            DelRelationSql.Append(" DELETE   RELATION  ");
            DelRelationSql.Append(" WHERE RTID IN ");
            DelRelationSql.Append(" (SELECT RTID FROM RELTYPE ");
            DelRelationSql.Append(" WHERE RT=:RT) ");


            StringBuilder DelRelTypeSql = new StringBuilder();
            DelRelTypeSql.Append(" DELETE   RELTYPE  ");
            DelRelTypeSql.Append(" WHERE RT=:RT");
            OracleParameter[] parameters = {
                            new OracleParameter("RT", OracleDbType.Varchar2,36)
                                            };
            parameters[0].Value = reltype.RT;

            Jurassic.GeoFeature.DBUtility.OracleDBHelper.SQLEntity DelRelationEntity = new DBUtility.OracleDBHelper.SQLEntity();
            DelRelationEntity.Sqlstr = DelRelationSql.ToString();
            DelRelationEntity.Oracleparameter = parameters;
            Jurassic.GeoFeature.DBUtility.OracleDBHelper.SQLEntity DelRelTypeEntity = new DBUtility.OracleDBHelper.SQLEntity();
            DelRelTypeEntity.Sqlstr = DelRelTypeSql.ToString();
            DelRelTypeEntity.Oracleparameter = parameters;
            sqlList.Add(DelRelationEntity);
            sqlList.Add(DelRelTypeEntity);
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteSql(sqlList) == true ? 1 : 0;
        }


        public List<string> GetBOTbyName(string BOTName1, string BOTName2)
        {
            string strSql = "select botid from( " +
                          "  select Rownum,a.*  from( " +
                          "  select botid from objecttype t  where t.bot='" + BOTName1 + "' " +
                          "  union all " +
                          "  select '0' from dual t  ) a " +
                          "   ) b  where Rownum=1" +
                          "   union all " +
                          "   select botid from( " +
                          "   select Rownum,a.*  from( " +
                          "   select botid from objecttype t  where t.bot='" + BOTName2 + "' " +
                          "   union all " +
                          "   select '0' from dual t  ) a " +
                          "   ) b  where Rownum=1";
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText<string>(strSql.ToString()); ;
        }

        public List<string> GetBOTRelByName(string BOTName1, string BOTName2)
        {
            string strSql = "select  decode(USEGEOMETRY,1,'1',0,botid ) botid from objecttype t  where t.bot='" + BOTName1 + "' " +
                          "  union all " +
                          "  select  decode(USEGEOMETRY,1,'1',0,botid ) botid from objecttype t  where t.bot='" + BOTName2 + "' ";
                        
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText<string>(strSql.ToString()); ;
        }

        public List<RelTypeModel> GetRelTypeNameByID(string BOTID)
        {
            string Sql = "select *  from RELTYPE where BOTID1='" + BOTID + "' " +
                       " or BOTID2='" + BOTID + "'";
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText<RelTypeModel>(Sql);
        }

        /// <summary>
        /// 根据对象类型获取对象关系类型
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="direction">关联方向，包括正向（Forward），反向（Backward），如果为空则两个方向都查询</param>
        /// <returns></returns>
        public IList<RelTypeModel> GetListByBot(string bot, string direction = null)
        {
            StringBuilder strSql = new StringBuilder();
            if (direction == "Forward")
                strSql.Append("SELECT * FROM RELTYPE R WHERE R.BOT1=:BOT");
            else if (direction == "Backward")
                strSql.Append("SELECT * FROM RELTYPE R WHERE R.BOT2=:BOT");
            else
            {
                strSql.Append("SELECT * FROM RELTYPE R WHERE R.BOT1=:BOT");
                strSql.Append(" UNION ");
                strSql.Append("SELECT * FROM RELTYPE R WHERE R.BOT2=:BOT");
            }
            OracleParameter[] parameters = {
                            new OracleParameter("BOT", OracleDbType.Varchar2,36)
                                            };
            parameters[0].Value = bot;
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText<RelTypeModel>(strSql.ToString());
        }

        /// <summary>
        /// 根据id查找对象关系类型
        /// </summary>
        /// <param name="rtid"></param>
        /// <returns></returns>
        public IList<RelTypeModel> GetListByID(string rtid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM RELTYPE R WHERE RT=:RT");
            OracleParameter[] parameters = {
                            new OracleParameter("RT", OracleDbType.Varchar2,36)
                                            };
            parameters[0].Value = rtid;
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText<RelTypeModel>(strSql.ToString());
        }

        /// <summary>
        /// 根据名称查找对象关系类型
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public IList<RelTypeModel> GetListByName(string title)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM RELTYPE R WHERE TITLE=:TITLE");
            OracleParameter[] parameters = {
                            new OracleParameter("TITLE", OracleDbType.Varchar2,36)
                                            };
            parameters[0].Value = title;
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText<RelTypeModel>(strSql.ToString());
        }

        public IList<RelTypeModel> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT T.RTID,T.BOTID1,T.BOTID2,T.RT AS RT,T1.BOT AS BOT1,T2.BOT AS BOT2,t.REMARK as Title  FROM RELTYPE T, ");
            strSql.Append(" OBJECTTYPE T1, OBJECTTYPE T2 ");
            strSql.Append(" WHERE T.BOTID1 = T1.BOTID ");
            strSql.Append(" AND T.BOTID2 = T2.BOTID ");
            strSql.Append(" ORDER BY RT ");
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText<RelTypeModel>(strSql.ToString());
        }


        /// <summary>
        /// 添加对象类型关系,包括实例的关系
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int AddRelationModel(List<RelTypeModel> list)
        {
            List<Jurassic.GeoFeature.DBUtility.OracleDBHelper.SQLEntity> sqlList = new List<Jurassic.GeoFeature.DBUtility.OracleDBHelper.SQLEntity>();

            foreach (var item in list)
            {
                Jurassic.GeoFeature.DBUtility.OracleDBHelper.SQLEntity sqlEntity = new DBUtility.OracleDBHelper.SQLEntity();
                StringBuilder strInsertSql = new StringBuilder();
                strInsertSql.Append(" INSERT INTO RELTYPE( ");
                strInsertSql.Append(" RTID,RT,REMARK,BOTID1,BOTID2) ");
                strInsertSql.Append(" VALUES (:RTID,:RT,:REMARK,:BOTID1,:BOTID2) ");

                OracleParameter[] parameters = {
                            new OracleParameter("RTID", OracleDbType.Varchar2,36),
                            new OracleParameter("RT", OracleDbType.Varchar2,36),    
                            new OracleParameter("REMARK", OracleDbType.Varchar2,100),    
                            new OracleParameter("BOTID1", OracleDbType.Varchar2,36),
                            new OracleParameter("BOTID2", OracleDbType.Varchar2,36)
                            };
                parameters[0].Value = item.Rtid;
                parameters[1].Value = item.RT;
                parameters[2].Value = item.Title;
                parameters[3].Value = item.Botid1;
                parameters[4].Value = item.Botid2;
                sqlEntity.Sqlstr = strInsertSql.ToString();
                sqlEntity.Oracleparameter = parameters;
                sqlList.Add(sqlEntity);
            }
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteSql(sqlList) == true ? 1 : 0;
        }

        /// <summary>
        /// 获取全部关系类型
        /// </summary>
        /// <returns></returns>
        public IList<RelTypeModel> GetAllRelationRT()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT A.RTID,A.RT ");
            strSql.Append(" FROM RELTYPE A,");
            strSql.Append("(SELECT MAX(RTID) RTID ");
            strSql.Append(" FROM RELTYPE");
            strSql.Append(" GROUP BY RT) B ");
            strSql.Append(" WHERE A.RTID  = B.RTID ");
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText<RelTypeModel>(strSql.ToString());
        }

        /// <summary>
        /// 根据对象类型关系rt获取对象类型关系实例
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        public IList<ObjTypeRelationModel> GetRelationListByrt(string rt)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT T.RTID,T.RT,T.BOTID1,T.BOTID2, T1.BOT AS BOT1, T2.BOT AS BOT2");
            strSql.Append(" FROM RELTYPE T, OBJECTTYPE T1, OBJECTTYPE T2 ");
            strSql.Append(" WHERE T.BOTID1 = T1.BOTID ");
            strSql.Append(" AND T.BOTID2 = T2.BOTID ");
            strSql.Append(" AND T.RT = :RT ");
            OracleParameter[] parameters = {
                            new OracleParameter("RT", OracleDbType.Varchar2,36)
                                            };
            parameters[0].Value = rt;
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText<ObjTypeRelationModel>(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据对象关系实例ID删除对象关系
        /// </summary>
        /// <param name="rtid"></param>
        /// <returns></returns>
        public int DeleteRelTypeByRtID(string rtid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DELETE FROM  RELTYPE  ");
            strSql.Append(" WHERE RTID=:RTID");
            OracleParameter[] parameters = {
                            new OracleParameter("RTID", OracleDbType.Varchar2,36)
                                            };
            parameters[0].Value = rtid;
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 修改对象关系类型
        /// </summary>
        /// <param name="list"></param>
        /// <param name="oldRT"></param>
        /// <returns></returns>
        public int UpdateRelTypeByRt(List<RelTypeModel> list, string oldRT)
        {
            //1.根据RT查询系统中所有的对象类型 2.对比修改、添加、删除
            IList<ObjTypeRelationModel> RelationList = GetRelationListByrt(oldRT);
            List<Jurassic.GeoFeature.DBUtility.OracleDBHelper.SQLEntity> sqlList = new List<Jurassic.GeoFeature.DBUtility.OracleDBHelper.SQLEntity>();

            //修改关系类型的实例
            foreach (var item in list)
            {
                ObjTypeRelationModel seachModel = (from p in RelationList where p.Botid1 == item.Botid1 && p.Botid2 == item.Botid2 && p.Rt == oldRT select p).FirstOrDefault();
                if (seachModel != null)
                {
                    #region 修改部分
                    Jurassic.GeoFeature.DBUtility.OracleDBHelper.SQLEntity sqlUpdateEntity = new DBUtility.OracleDBHelper.SQLEntity();
                    StringBuilder UpdateRelTypeSql = new StringBuilder();
                    UpdateRelTypeSql.Append(" UPDATE RELTYPE ");
                    UpdateRelTypeSql.Append(" SET TITLE=:TITLE,RT=:RT ");
                    UpdateRelTypeSql.Append(" WHERE RTID=:RTID ");
                    OracleParameter[] parameters = {
                            new OracleParameter("TITLE", OracleDbType.Varchar2,36),
                            new OracleParameter("RT", OracleDbType.Varchar2,36),
                            new OracleParameter("RTID", OracleDbType.Varchar2,36)
                                            };
                    parameters[0].Value = item.Title;
                    parameters[1].Value = item.RT;
                    parameters[2].Value = item.Rtid;
                    sqlUpdateEntity.Sqlstr = UpdateRelTypeSql.ToString();
                    sqlUpdateEntity.Oracleparameter = parameters;
                    sqlList.Add(sqlUpdateEntity);
                    #endregion
                }
                else
                {
                    #region 新增部分
                    Jurassic.GeoFeature.DBUtility.OracleDBHelper.SQLEntity sqlAddEntity = new DBUtility.OracleDBHelper.SQLEntity();
                    StringBuilder strInsertSql = new StringBuilder();
                    strInsertSql.Append(" INSERT INTO RELTYPE( ");
                    strInsertSql.Append(" RTID,RT,TITLE,BOTID1,BOTID2) ");
                    strInsertSql.Append(" VALUES (:RTID,:RT,:TITLE,:BOTID1,:BOTID2) ");

                    OracleParameter[] parameters = {
                            new OracleParameter("RTID", OracleDbType.Varchar2,36),
                            new OracleParameter("RT", OracleDbType.Varchar2,36),
                            new OracleParameter("TITLE", OracleDbType.Varchar2,100),
                            new OracleParameter("BOTID1", OracleDbType.Varchar2,36),
                            new OracleParameter("BOTID2", OracleDbType.Varchar2,36)
                            };
                    parameters[0].Value = System.Guid.NewGuid().ToString();
                    parameters[1].Value = item.RT;
                    parameters[2].Value = item.Title;
                    parameters[3].Value = item.Botid1;
                    parameters[4].Value = item.Botid2;
                    sqlAddEntity.Sqlstr = strInsertSql.ToString();
                    sqlAddEntity.Oracleparameter = parameters;
                    sqlList.Add(sqlAddEntity);
                    #endregion
                }
            }

            #region 删除部分
            foreach (var item in RelationList)
            {
                RelTypeModel seachModel = (from p in list where p.Botid1 == item.Botid1 && p.Botid2 == item.Botid2 && item.Rt == oldRT select p).FirstOrDefault();
                if (seachModel == null)
                {
                    //删除对象关系实例
                    Jurassic.GeoFeature.DBUtility.OracleDBHelper.SQLEntity sqlDelEntity = new DBUtility.OracleDBHelper.SQLEntity();
                    StringBuilder DelRelationSql = new StringBuilder();
                    DelRelationSql.Append(" DELETE RELATION ");
                    DelRelationSql.Append(" WHERE RTID =(SELECT RTID FROM RELTYPE WHERE BOTID1 =:BOTID1 AND BOTID2=:BOTID2 AND RT=:RT) ");
                    OracleParameter[] parameters = {
                            new OracleParameter("BOTID1", OracleDbType.Varchar2,36),
                            new OracleParameter("BOTID2", OracleDbType.Varchar2,36),
                            new OracleParameter("RT", OracleDbType.Varchar2,50)
                                                        };
                    parameters[0].Value = item.Botid1;
                    parameters[1].Value = item.Botid2;
                    parameters[2].Value = oldRT;
                    sqlDelEntity.Sqlstr = DelRelationSql.ToString();
                    sqlDelEntity.Oracleparameter = parameters;
                    sqlList.Add(sqlDelEntity);

                    //删除类型关系实例
                    Jurassic.GeoFeature.DBUtility.OracleDBHelper.SQLEntity Entity = new DBUtility.OracleDBHelper.SQLEntity();
                    StringBuilder delRelTypeSql = new StringBuilder();
                    delRelTypeSql.Append(" DELETE RELTYPE ");
                    delRelTypeSql.Append(" WHERE RTID =(SELECT RTID FROM RELTYPE WHERE BOTID1 =:BOTID1 AND BOTID2=:BOTID2 AND RT=:RT) ");
                    OracleParameter[] parameters1 = {
                            new OracleParameter("BOTID1", OracleDbType.Varchar2,36),
                            new OracleParameter("BOTID2", OracleDbType.Varchar2,36),
                            new OracleParameter("RT", OracleDbType.Varchar2,50)
                                                        };
                    parameters1[0].Value = item.Botid1;
                    parameters1[1].Value = item.Botid2;
                    parameters1[2].Value = oldRT;
                    Entity.Sqlstr = delRelTypeSql.ToString();
                    Entity.Oracleparameter = parameters1;
                    sqlList.Add(Entity);
                }
            }
            #endregion
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteSql(sqlList) == true ? 1 : 0;
        }

        /// <summary>
        /// 获取全部对象类型关系
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllRelation()
        {
            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            dt1 = DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText(" select DISTINCT RT from reltype ").Tables[0];
            dt1.TableName = "reltypeRT";
            DataTable dt = new DataTable();
            dt = DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText(" select *  from reltype ").Tables[0];
            dt.TableName = "reltypeAll";
            ds.Tables.Add(dt1.Copy());
            ds.Tables.Add(dt.Copy());
            return ds;
        }

        /// <summary>
        /// 获取所有关系类型树，或者根据节点取得子树
        /// </summary>
        /// <param name="rootList"></param>
        /// <returns></returns>
        public IList<RelTypeModel> GetRelTree(List<string> rootList = null)
        {
            StringBuilder sql = new StringBuilder();
            var oraParams = new List<OracleParameter>();
            sql.Append("SELECT * FROM RELTYPE R START WITH R.BOTID1 IN");
            if (rootList == null)
            {
                sql.Append("(SELECT A.BOTID1 FROM RELTYPE A WHERE A.BOTID1 NOT IN (SELECT B.BOTID2 FROM RELTYPE B))");
            }
            else
            {
                sql.Append("(:BOTID1)");
                var counter = 0;
                var collectionParams = new StringBuilder(":");
                foreach (var obj in rootList)
                {
                    var param = "BOTID1" + counter;
                    collectionParams.Append(param);
                    collectionParams.Append(", :");
                    oraParams.Add(new OracleParameter(param, OracleDbType.Varchar2) { Value = obj });
                    counter++;
                }
                collectionParams.Remove(collectionParams.Length - 3, 3);

                sql = sql.Replace(":" + "BOTID1", collectionParams.ToString());
            }
            sql.Append("CONNECT BY R.BOTID1 = PRIOR R.BOTID2");

            return DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText<RelTypeModel>(sql.ToString(), oraParams.ToArray());
        }

        public IList<RelTypeModel> GetRelTreeRoot()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT A.* FROM RELTYPE A WHERE A.BOTID1 NOT IN (SELECT B.BOTID2 FROM RELTYPE B)");
            return DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText<RelTypeModel>(sql.ToString());
        }

        public IList<RelTypeModel> GetRelTreeSubNode(string botid1)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT A.* FROM RELTYPE A WHERE A.BOTID1 =:BOTID1");
            var oraParams = new List<OracleParameter>();
            oraParams.Add(new OracleParameter("BOTID1", OracleDbType.Varchar2) { Value = botid1 });

            return DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText<RelTypeModel>(sql.ToString(), oraParams.ToArray());
        }

        public DataTable GetRelTable(string RTID)
        {
            DataSet ds = new DataSet();
            string Sql = "select RTID,关系名称,关系类型,对象类型1,对象类型2 from( " +
                           " select  '0' SortID,  RTID , REMARK as 关系名称,RT as 关系类型, b.bot as 对象类型1,c1.bot as 对象类型2  from " +
                           " reltype  a ,objecttype b,objecttype c1 " +
                           "  where  RTID='" + RTID + "'  " +
                           "  and a.botid1=b.botid and a.botid2=c1.botid " +
                           "  union all " +
                           " select '1' SortID,RELATIONID RTID ,'' 关系名称,'' 关系类型,c.name 对象类型1,d.name 对象类型1   " +
                           " from relation b ,bo c ,bo d " +
                           "  where RTID='" + RTID + "' and  " +
                           " b.boid1=c.boid and b.boid2=d.boid " +
                           " ) order by SortID asc";

            ds = DBUtility.OracleDBHelper.OracleHelper.ExecuteQueryText(Sql);
            if (ds != null)
                return ds.Tables[0];
            else
                return null;
        }

        public bool DelBOTRel(List<string> RTID, string BOTorBO)
        {
            List<string> ListSql = new List<string>();
            string Sql = "";
            if (BOTorBO == "BOT")//删除对象类型及该类型下对象实例
            {
                Sql = "delete from Relation where RELATIONID='" + RTID[0] + "'";
                ListSql.Add(Sql);

                Sql = "delete from RelType where RTID='" + RTID[0] + "'";
                ListSql.Add(Sql);
            }
            else//只删除对象实例
            {
                foreach (string tmplist in RTID)
                {
                    Sql = "delete from Relation where RELATIONID='" + tmplist + "'";
                    ListSql.Add(Sql);
                }
            }
            string[] Sqls = ListSql.ToArray();
            if (Sqls.Length > 0)
                return DBUtility.OracleDBHelper.OracleHelper.ExecuteSql(Sqls);
            else
                return false;
        }

    }
}