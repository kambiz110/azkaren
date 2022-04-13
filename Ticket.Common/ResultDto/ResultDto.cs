using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Common.ResultDto
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}

namespace Azmoon.Common.ResultDto
{
    public class ResultDto<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
    public class ResultDto<T , D>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public D TwoDate { get; set; }
    }
    public class ResultUploadFileDto
    {
        public bool Status { get; set; }
        public string FileNameAddress { get; set; }
        public string FileNameAddressResize { get; set; }
    }
    public class PagerDto
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
    }
}
