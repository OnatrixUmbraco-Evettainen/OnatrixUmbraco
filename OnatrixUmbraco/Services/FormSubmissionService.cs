using OnatrixUmbraco.ViewModels;
using Umbraco.Cms.Core.Services;

namespace OnatrixUmbraco.Services;

public class FormSubmissionService(IContentService contentService)
{

    private readonly IContentService _contentService = contentService;

    public bool SaveRequestsForm(FormCallBackViewModel form)
    {
        try
        {
            var requestsGuid = new Guid("312caf86-50b2-4592-af30-a7378401f456");
            var requestsId = _contentService.GetById(requestsGuid);
            var requestsItem = _contentService.Create(DateTime.Now.ToString(), requestsId, "requestsItem");
            requestsItem.SetValue("requestsName", form.Name);
            requestsItem.SetValue("requestsPhone", form.Phone);
            requestsItem.SetValue("requestsEmail", form.Email);
            requestsItem.SetValue("requestsOptionSelected", form.SelectedOption);

            _contentService.Save(requestsItem);
            _contentService.Publish(requestsItem, []);
            return true;
        }
        catch (Exception ex)
        { 
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool SaveSupportsForm(FormSupportViewModel form)
    {
        try
        {
            var supportsGuid = new Guid("4ba3ac30-1071-453e-9b3e-6eee933700f2");
            var supporstId = _contentService.GetById(supportsGuid);
            var supportsItem = _contentService.Create(DateTime.Now.ToString(), supporstId, "supportsItem");
            supportsItem.SetValue("requestsEmail", form.Email);

            _contentService.Save(supportsItem);
            _contentService.Publish(supportsItem, []);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool SaveQuestionsForm(FormQuestionViewModel form)
    {
        try
        {
            var questionsGuid = new Guid("b7c188fc-445d-4903-b1e5-a44331777745");
            var questionsId = _contentService.GetById(questionsGuid);
            var questionsItems = _contentService.Create(DateTime.Now.ToString(), questionsId, "questionsItems");
            questionsItems.SetValue("questionsName", form.Name);
            questionsItems.SetValue("questionsEmail", form.Email);
            questionsItems.SetValue("questionsQuestion", form.Question);

            _contentService.Save(questionsItems);
            _contentService.Publish(questionsItems, []);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
