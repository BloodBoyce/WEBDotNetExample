namespace QuestionsApp.Handlers.Queries;

using MediatR;

public class GetQuestionsResponse
{
    public int Id { get; set; }
    public string Content { get; set; } = "";
    public int Votes { get; set; }
}

public class GetQuestionsRequest : IRequest<List<GetQuestionsResponse>>
{ }

public class GetQuestionsQuery : IRequestHandler<GetQuestionsRequest, List<GetQuestionsResponse>>
{
    public Task<List<GetQuestionsResponse>> Handle(GetQuestionsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}