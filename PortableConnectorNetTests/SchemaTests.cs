﻿// Copyright © 2015, Oracle and/or its affiliates. All rights reserved.
//
// MySQL Connector/NET is licensed under the terms of the GPLv2
// <http://www.gnu.org/licenses/old-licenses/gpl-2.0.html>, like most 
// MySQL Connectors. There are special exceptions to the terms and 
// conditions of the GPLv2 as it is applied to this software, see the 
// FLOSS License Exception
// <http://www.mysql.com/about/legal/licensing/foss-exception.html>.
//
// This program is free software; you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published 
// by the Free Software Foundation; version 2 of the License.
//
// This program is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License 
// for more details.
//
// You should have received a copy of the GNU General Public License along 
// with this program; if not, write to the Free Software Foundation, Inc., 
// 51 Franklin St, Fifth Floor, Boston, MA 02110-1301  USA

using MySql.XDevAPI;
using System;
using System.Collections.Generic;
using Xunit;

namespace PortableConnectorNetTests
{
  public class SchemaTests : BaseTest
  {
    [Fact]
    public void GetSchemas()
    {
      Session session = GetSession();
      List<Schema> schemas = session.GetSchemas();

      Assert.Equal(5, schemas.Count);
    }

    [Fact]
    public void GetInvalidSchema()
    {
      Session s = GetSession();
      Schema schema = s.GetSchema("test-schema");
      Assert.False(schema.ExistsInDatabase());
    }

    [Fact]
    public void GetAllTables()
    {
      Schema schema = SetupSchema("test");
      Collection coll = CreateCollection("coll");
      ExecuteSQL("CREATE TABLE test.test(id int)");

      List<Table> tables = schema.GetTables();
      Assert.True(tables.Count == 1);
    }

    [Fact]
    public void GetAllViews()
    {
      Schema schema = SetupSchema("test");
      Collection coll = CreateCollection("coll");

      ExecuteSQL("CREATE TABLE test.test(id int)");
      ExecuteSQL("CREATE VIEW test.view1 AS select * from test.test");

      List<View> views = schema.GetViews();
      Assert.True(views.Count == 1);
    }
  }
}
