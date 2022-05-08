namespace MediatR.Extensions.Cqs;

/// <summary>
/// Marker interface to represent a query with a void response
/// </summary>
public interface IQuery : IRequest<Unit> { }

/// <summary>
/// Marker interface to represent a query with a response
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse>, IBaseQuery { }

/// <summary>
/// Allows for generic type constraints of objects implementing IQuery or IQuery{TResponse} 
/// </summary>
public interface IBaseQuery : IBaseRequest { }