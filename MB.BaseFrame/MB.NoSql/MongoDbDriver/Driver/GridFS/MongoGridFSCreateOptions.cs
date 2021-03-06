﻿/* Copyright 2010-2011 10gen Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace MongoDB.Driver.GridFS {
    /// <summary>
    /// Represents options used when creating a GridFS file.
    /// </summary>
    public class MongoGridFSCreateOptions {
        #region private fields
        private string[] aliases;
        private int chunkSize;
        private string contentType;
        private BsonValue id; // usually a BsonObjectId but not required to be
        private BsonDocument metadata;
        private DateTime uploadDate;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the MongoGridFSCreateOptions class.
        /// </summary>
        public MongoGridFSCreateOptions() {
        }
        #endregion

        #region public properties
        /// <summary>
        /// Gets or sets the aliases.
        /// </summary>
        public string[] Aliases {
            get { return aliases; }
            set { aliases = value; }
        }

        /// <summary>
        /// Gets or sets the chunk size.
        /// </summary>
        public int ChunkSize {
            get { return chunkSize; }
            set { chunkSize = value; }
        }

        /// <summary>
        /// Gets or sets the content type.
        /// </summary>
        public string ContentType {
            get { return contentType; }
            set { contentType = value; }
        }

        /// <summary>
        /// Gets or sets the file Id.
        /// </summary>
        public BsonValue Id {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        public BsonDocument Metadata {
            get { return metadata; }
            set { metadata = value; }
        }

        /// <summary>
        /// Gets or sets the upload date.
        /// </summary>
        public DateTime UploadDate {
            get { return uploadDate; }
            set { uploadDate = value; }
        }
        #endregion
    }
}
