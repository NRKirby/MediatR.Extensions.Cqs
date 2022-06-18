namespace MediatR.Extensions.Cqs;

/// <summary>
/// Marker interface to represent a command with a void response
/// </summary>
public interface ICommand : IRequest<Unit>, IBaseCommand { }

/// <summary>
/// Marker interface to represent a command with a response
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>, IBaseCommand { }

/// <summary>
/// Allows for generic type constraints of objects implementing ICommand or ICommand{TResponse} 
/// </summary>
public interface IBaseCommand : IBaseRequest { }