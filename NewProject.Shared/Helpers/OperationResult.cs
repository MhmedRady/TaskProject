using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Shared;

public class OperationResult
{
    public OperationResult(bool success, string messag, dynamic payload)
    {
        this.Success = success;
        this.Message = messag;
        this.Payload = payload;
    }
    public bool Success { get; set; }
    public string Message { get; set; }

    public dynamic Payload { get; set; }

    public static OperationResult Succeeded(string msg = "تمت العملية بنجاح!.", dynamic payload = null)
    {
        return new OperationResult(true, msg, payload);
    }
    public static OperationResult NotFound(string msg = "العنصر المطلوب غير موجود!.", dynamic payload = null)
    {
        return new OperationResult(false, msg, payload);
    }
    public static OperationResult Failed(string msg = "تم فشل العملية!.", dynamic payload = null)
    {
        return new OperationResult(false, msg, payload);
    }

    public static OperationResult Existed(string msg = "هذا العنصر موجود من قبل!.", dynamic payload = null)
    {
        return new OperationResult(false, msg, payload);
    }

    public static OperationResult Cascading(string msg = "يجب أن تحذف المتفرعات أولاً")
    {
        return new OperationResult(false, msg, null);
    }

    // Add More Results .......

}
