﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Data;
using Microsoft.Health.Fhir.Core.Features.Search.SearchValues;

namespace Microsoft.Health.Fhir.BulkImportDemoWorker.SearchParamGenerator
{
    public class StringSearchParamGenerator : ISearchParamGenerator
    {
        private ModelProvider _modelProvider;

        public StringSearchParamGenerator(ModelProvider modelProvider)
        {
            _modelProvider = modelProvider;
        }

        public string TableName => "dbo.StringSearchParam";

        public DataTable CreateDataTable()
        {
            DataTable table = new DataTable("DataTable");
            DataColumn column;

            column = new DataColumn();
            column.DataType = typeof(short);
            column.ColumnName = "ResourceTypeId";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(long);
            column.ColumnName = "ResourceSurrogateId";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(short);
            column.ColumnName = "SearchParamId";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Text";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "TextOverflow";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "IsHistory";
            column.ReadOnly = true;
            table.Columns.Add(column);

            return table;
        }

        public DataRow GenerateDataRow(DataTable table, BulkCopySearchParamWrapper searchParam)
        {
            StringSearchValue searchValue = (StringSearchValue)searchParam.SearchIndexEntry.Value;

            DataRow row = table.NewRow();
            row["ResourceTypeId"] = _modelProvider.ResourceTypeMapping[searchParam.Resource.InstanceType];
            row["ResourceSurrogateId"] = searchParam.SurrogateId;
            row["SearchParamId"] = _modelProvider.SearchParamTypeMapping.ContainsKey(searchParam.SearchIndexEntry.SearchParameter.Url) ? _modelProvider.SearchParamTypeMapping[searchParam.SearchIndexEntry.SearchParameter.Url] : 0;
            row["IsHistory"] = false;
            FillInRow(row, searchValue);

            return row;
        }

        public static void FillInRow(DataRow row, StringSearchValue searchValue, string index = "")
        {
            string content = searchValue.String;
            string overflow;
            string indexedPrefix;

            if (content.Length > 256)
            {
                indexedPrefix = content.Substring(0, 256);
                overflow = content;
            }
            else
            {
                indexedPrefix = content;
                overflow = null;
            }

            row["Text" + index] = indexedPrefix;
            row["TextOverflow" + index] = overflow;
        }
    }
}
