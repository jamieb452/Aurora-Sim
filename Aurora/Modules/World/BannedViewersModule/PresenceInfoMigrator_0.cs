/*
 * Copyright 2011 Matthew Beardmore
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Aurora-Sim Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using Aurora.Framework;
using Aurora.DataManager.Migration;
using Aurora.Framework.Utilities;

namespace Aurora.Modules.Ban
{
    public class PresenceInfoMigrator_0 : Migrator
    {
        private static readonly List<SchemaDefinition> _schema = new List<SchemaDefinition>()
        {
            new SchemaDefinition("baninfo",  
                new ColumnDefinition[]
                {
                    new ColumnDefinition {Name = "AgentID", Type = ColumnTypeDef.Char36},
                    new ColumnDefinition {Name = "Flags", Type = ColumnTypeDef.String50},
                    new ColumnDefinition {Name = "KnownAlts", Type = ColumnTypeDef.LongText},
                    new ColumnDefinition {Name = "KnownID0s", Type = ColumnTypeDef.LongText},
                    new ColumnDefinition {Name = "KnownIPs", Type = ColumnTypeDef.LongText},
                    new ColumnDefinition {Name = "KnownMacs", Type = ColumnTypeDef.LongText},
                    new ColumnDefinition {Name = "KnownViewers", Type = ColumnTypeDef.LongText},
                    new ColumnDefinition {Name = "LastKnownID0", Type = ColumnTypeDef.String50},
                    new ColumnDefinition {Name = "LastKnownIP", Type = ColumnTypeDef.String50},
                    new ColumnDefinition {Name = "LastKnownMac", Type = ColumnTypeDef.String50},
                    new ColumnDefinition {Name = "LastKnownViewer", Type = ColumnTypeDef.String255},
                    new ColumnDefinition {Name = "Platform", Type = ColumnTypeDef.String50},
                },
                new IndexDefinition[] 
                {
                    new IndexDefinition() { Fields = new string[] {"AgentID"}, Type = IndexType.Primary }
                }),
        };

        public PresenceInfoMigrator_0()
        {
            Version = new Version(0, 1, 0);
            MigrationName = "PresenceInfo";
            base.schema = _schema;
        }

        protected override void DoCreateDefaults(IDataConnector genericData)
        {
            EnsureAllTablesInSchemaExist(genericData);
        }

        protected override bool DoValidate(IDataConnector genericData)
        {
            return TestThatAllTablesValidate(genericData);
        }

        protected override void DoMigrate(IDataConnector genericData)
        {
            DoCreateDefaults(genericData);
        }

        protected override void DoPrepareRestorePoint(IDataConnector genericData)
        {
            CopyAllTablesToTempVersions(genericData);
        }

        public override void FinishedMigration(IDataConnector genericData)
        {
            genericData.Delete("baninfo", null);
        }
    }
}