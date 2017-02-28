using SimpleStack.Orm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using SimpleStack.Orm.Attributes;
using SimpleStack.Orm.Expressions;
using System.Linq.Expressions;
using Ticket.Base;

namespace Dapper
{
    public static class SqlMapper
    {

        public static async Task<PaginationQuery<T>> QueryPagination<T>(this OrmConnection cnn,
            string selectExpression,
            string whereExpression,
            int? startPageNo = null,
            int? endPageNo = null,
            int? pageSize = null,
            object parameters = null,
            bool runCount = false)
        {
            return await QueryPagination<T>(cnn, null, selectExpression, whereExpression, "", startPageNo, endPageNo, pageSize, parameters, runCount);
        }

        public static async Task<PaginationQuery<T>> QueryPagination<T>(this OrmConnection cnn,
            string selectExpression,
            string whereExpression,
            string countExpression,
            int? startPageNo = null,
            int? endPageNo = null,
            int? pageSize = null,
            object parameters = null,
            bool runCount = false)
        {
            return await QueryPagination<T>(cnn, null, selectExpression, whereExpression, countExpression, startPageNo, endPageNo, pageSize, parameters, runCount);
        }

        public static async Task<PaginationQuery<T>> QueryPagination<T>(this OrmConnection cnn,
          int? startPageNo = null,
          int? endPageNo = null,
          int? pageSize = null,
          object parameters = null,
          bool runCount = false)
        {
            return await QueryPagination<T>(cnn, null , startPageNo, endPageNo, pageSize, parameters, runCount);
        }

        public static async Task<PaginationQuery<T>> QueryPagination<T>(this OrmConnection cnn,
            string whereExpression,
            int? startPageNo,
            int? endPageNo,
            int? pageSize,
            object parameters,
            bool runCount = false)
        {
            var expressionVisitor = cnn.DialectProvider.ExpressionVisitor<T>();
            return await QueryPagination<T>(cnn, expressionVisitor, null, whereExpression, startPageNo, endPageNo, pageSize, parameters, runCount);
        }

        public static async Task<PaginationQuery<T>> QueryPagination<T>(this OrmConnection cnn,
            SqlExpressionVisitor<T> expressionVisitor,
           string selectExpression = "",
           string whereExpression = "",
           string countExpression = "",
           int? startPageNo = null,
           int? endPageNo = null,
           int? pageSize = null,
           object parameters = null,
           
           bool runCount = false)
        {
            return await QueryPagination<T>(cnn,
               expressionVisitor,
               selectExpression,
               whereExpression,
               countExpression,
               "",
               "",
               "",
               startPageNo,
               endPageNo,
               pageSize,
               parameters,
               runCount);
        }

        public static async Task<PaginationQuery<T>> QueryPagination<T>(this OrmConnection cnn,
           SqlExpressionVisitor<T> expressionVisitor,
           string selectExpression = "",
           string whereExpression = "",
           int? startPageNo = null,
           int? endPageNo = null,
           int? pageSize = null,
           object parameters = null,
           bool runCount = false)
        {
            return await QueryPagination<T>(cnn,
               expressionVisitor,
               selectExpression,
               whereExpression,
               "",
               "",
               "",
               "",
               startPageNo,
               endPageNo,
               pageSize,
               expressionVisitor.Parameters,
               runCount);
        }
        public static async Task<PaginationQuery<T>> QueryPagination<T>(this OrmConnection cnn,
            SqlExpressionVisitor<T> expressionVisitor,
            string selectExpression = "",
            string whereExpression = "",
            string countExpression = "",
            string groupByExpression = "",
            string havingExpression = "",
            string orderByExpression = "",
            int? startPageNo = null,
            int? endPageNo = null,
            int? pageSize = null,
            object parameters = null,
            bool runCount = false)
        {
            if (expressionVisitor == null)
                expressionVisitor = cnn.DialectProvider.ExpressionVisitor<T>();

            if (string.IsNullOrEmpty(selectExpression))
                selectExpression = expressionVisitor.SelectExpression;

            if (string.IsNullOrEmpty(whereExpression))
                whereExpression = expressionVisitor.WhereExpression;

            if (string.IsNullOrEmpty(havingExpression))
                havingExpression = expressionVisitor.HavingExpression;

            if (string.IsNullOrEmpty(groupByExpression))
                groupByExpression = expressionVisitor.GroupByExpression;


            if (string.IsNullOrEmpty(orderByExpression))
                orderByExpression = expressionVisitor.OrderByExpression;


            ModelDefinition modelDef = ModelDefinition<T>.Definition;
            string tableName = cnn.DialectProvider.GetQuotedTableName(modelDef);

            string countQuery = countExpression;
            if (string.IsNullOrEmpty(countQuery))
            {
                countQuery = "SELECT COUNT(*) FROM " + tableName;
            }

            var startPageValue = startPageNo.GetValueOrDefault(1);
            var endPageValue = endPageNo.GetValueOrDefault(1);
            var pageSizevalue = pageSize.GetValueOrDefault(30);

            var limit = expressionVisitor.Limit((startPageValue - 1) * pageSizevalue,
                (endPageValue - startPageValue + 1) * pageSize.GetValueOrDefault(30)).LimitExpression;

            var sql = selectExpression + " " + whereExpression + " " + orderByExpression + " " + limit;

            var data = await cnn.QueryAsync<T>(sql, parameters);
            long countValue = 0;
            if (startPageValue == 1 || runCount)
                countValue = await cnn.QueryFirstAsync<long>(countQuery + whereExpression, parameters);

            return new PaginationQuery<T>()
            {
                Data = data,
                TotalRecords = countValue
            };
        }


        public static async Task<PaginationQuery<T>> QueryPagination<T>(this OrmConnection cnn,
            Expression<Func<T, bool>> selectPredicate,
            int? startPageNo = null,
            int? endPageNo = null,
            int? pageSize = null,
            bool runCount = false)
        {
            var expressionvisitor = cnn.DialectProvider.ExpressionVisitor<T>();
            var whereExpression = expressionvisitor.Where(selectPredicate).WhereExpression;
            var selectExpression = expressionvisitor.SelectExpression;
            return await QueryPagination<T>(cnn,
                expressionvisitor,
                selectExpression,
                whereExpression,
                startPageNo,
                endPageNo,
                pageSize,
                expressionvisitor.Parameters,
                runCount);

        }

    }

    public class PaginationQuery<T> : IPaginationQuery<T>
    {
        public IEnumerable<T> Data { get; set; }
        public long TotalRecords { get; set; }
    }
}
