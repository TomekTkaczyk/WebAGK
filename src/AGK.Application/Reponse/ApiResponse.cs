﻿using System.Collections.ObjectModel;
using System.Net;
using System.Text.Json.Serialization;

namespace AGK.Application.Reponse;
public record ApiResponse
{
	public bool IsSuccess { get; init; }

	public string Message { get; init; }

	public HttpStatusCode HttpCode { get; init; }

	public ICollection<Error> Errors { get; init; } = [];

	[JsonConstructor]
	public ApiResponse(
		bool isSuccess,
		string message,
		HttpStatusCode httpCode,
		ICollection<Error> errors) {
		if(isSuccess && errors != null && errors.Count > 0) {
			throw new InvalidOperationException();
		}

		IsSuccess = isSuccess;
		Message = message;
		HttpCode = httpCode;
		Errors = errors;
	}


	public static ApiResponse Success()
	=> new(true, string.Empty, HttpStatusCode.NoContent, []);

	public static ApiResponse Success(HttpStatusCode httpCode)
	=> new(true, string.Empty, httpCode, new Collection<Error>());

	public static ApiResponse Failure(string message, HttpStatusCode httpCode, ICollection<Error> errors = null)
		=> new(false, message, httpCode, errors);

	public static ApiResponse<TValue> Success<TValue>(TValue value)
		=> new(value, true, "", HttpStatusCode.OK, new Collection<Error>());

	public static ApiResponse<TValue> Failure<TValue>(string message, HttpStatusCode httpCode)
	=> new(false, message, httpCode, new Collection<Error>());

	public static ApiResponse<TValue> Failure<TValue>(string message, HttpStatusCode httpCode, Error error = null)
	=> new(false, message, httpCode, new Collection<Error>() { error }.Where(x => x != null).ToList());

	public ApiResponse AddError(Error error) {
		Errors.Add(error);

		return this;
	}
}

public record ApiResponse<TValue> : ApiResponse
{
	private readonly TValue _value;

	[JsonConstructor]
	public ApiResponse(
		bool isSuccess,
		string message,
		HttpStatusCode httpCode,
		ICollection<Error> errors,
		TValue value) : base(isSuccess, message, httpCode, errors) {
		_value = value;
	}


	public TValue Value => IsSuccess
		? _value
		: throw new InvalidOperationException("The value of a failure result can not be accessed.");

	internal protected ApiResponse(TValue value, bool isSuccess, HttpStatusCode httpCode)
		: base(isSuccess, "", httpCode, new Collection<Error>()) => _value = value;

	internal protected ApiResponse(TValue value, bool isSuccess, string message, HttpStatusCode httpCode)
		: base(isSuccess, message, httpCode, new Collection<Error>()) => _value = value;

	internal protected ApiResponse(TValue value, bool isSuccess, string message, HttpStatusCode httpCode, ICollection<Error> errors)
		: base(isSuccess, message, httpCode, errors) => _value = value;

	internal protected ApiResponse(bool isSuccess, string message, HttpStatusCode httpCode, ICollection<Error> errors)
		: base(isSuccess, message, httpCode, errors) { }
}

