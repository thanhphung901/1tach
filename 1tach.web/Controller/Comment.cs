using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;

namespace Controller
{
    public class Comment
    {
        #region Decclare
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        #endregion
        public bool Addcomment(int news_id, string name, string desc, int type)
        {
            try
            {
                ESHOP_NEWS_COMMENT cm = new ESHOP_NEWS_COMMENT();
                cm.NEWS_ID = news_id;
                cm.COMMENT_CONTENT = desc;
                cm.COMMENT_NAME = name;
                cm.COMMENT_STATUS = 1;
                cm.COMMENT_TYPE = type;
                cm.COMMENT_PUBLISHDATE = DateTime.Now;
                db.ESHOP_NEWS_COMMENTs.InsertOnSubmit(cm);
                db.SubmitChanges();

                var updateNew = db.ESHOP_NEWs.Where(n => n.NEWS_ID == news_id).ToList();
                if (updateNew.Count > 0)
                {
                    int iDebateNo = Utils.CIntDef(updateNew[0].DEBATE_NO);
                    int iDebateYes = Utils.CIntDef(updateNew[0].DEBATE_YES);
                    if (type == 0)
                    {
                        updateNew[0].DEBATE_NO = iDebateNo + 1;
                    }
                    else if (type == 1)
                    {
                        updateNew[0].DEBATE_YES = iDebateYes + 1;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public List<ESHOP_NEWS_COMMENT> Load_comment(int newId, int type, int limit)
        {
            try
            {
                var show = (from a in db.ESHOP_NEWs
                            join b in db.ESHOP_NEWS_COMMENTs on a.NEWS_ID equals b.NEWS_ID
                            where a.NEWS_ID == newId 
                                && b.COMMENT_STATUS == 1
                                && (b.COMMENT_TYPE == type || type == -1)
                            select b).OrderByDescending(n => n.COMMENT_PUBLISHDATE).Take(limit).ToList();
                return show;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCountComment(int newId)
        {
            int _count = db.ESHOP_NEWS_COMMENTs.Count(n => n.NEWS_ID == newId);
            return _count;
        }
    }
}
