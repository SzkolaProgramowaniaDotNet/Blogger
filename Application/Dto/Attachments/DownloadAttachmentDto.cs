﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Attachments
{
    public class DownloadAttachmentDto : AttachmentDto
    {
        public byte[] Content { get; set; }
    }
}
