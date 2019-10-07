﻿//------------------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using System;
using Microsoft.IdentityModel.Protocols.SignedHttpRequest;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace Microsoft.IdentityModel.Protocols.SignedHttpRequest.Tests
{
    public class SignedHttpRequestDataTests
    {
        [Fact]
        public void SignedHttpRequestCreationData()
        {
            var httpRequestData = new HttpRequestData();
            var accessToken = SignedHttpRequestTestUtils.DefaultEncodedAccessToken;
            var signingCredentials = SignedHttpRequestTestUtils.DefaultSigningCredentials;
            var creationPolicy = new SignedHttpRequestCreationPolicy();
            var callContext = CallContext.Default;

            Assert.Throws<ArgumentNullException>("accessToken", () =>  new SignedHttpRequestCreationData(null, httpRequestData, null, null));
            Assert.Throws<ArgumentNullException>("accessToken", () => new SignedHttpRequestCreationData(null, httpRequestData, null, null, callContext));
            Assert.Throws<ArgumentNullException>("accessToken", () => new SignedHttpRequestCreationData(string.Empty, httpRequestData, null, null));
            Assert.Throws<ArgumentNullException>("signingCredentials", () => new SignedHttpRequestCreationData(accessToken, httpRequestData, null, null));
            Assert.Throws<ArgumentNullException>("signingCredentials", () => new SignedHttpRequestCreationData(accessToken, httpRequestData, null, null, callContext));
            Assert.Throws<ArgumentNullException>("signedHttpRequestCreationPolicy", () => new SignedHttpRequestCreationData(accessToken, httpRequestData, signingCredentials, null));
            Assert.Throws<ArgumentNullException>("signedHttpRequestCreationPolicy", () => new SignedHttpRequestCreationData(accessToken, httpRequestData, signingCredentials, null, callContext));
            Assert.Throws<ArgumentNullException>("callContext", () => new SignedHttpRequestCreationData(string.Empty, httpRequestData, null, null, null));
            Assert.Throws<ArgumentNullException>("callContext", () => new SignedHttpRequestCreationData(accessToken, httpRequestData, signingCredentials, creationPolicy, null));

            // no exceptions
            var creationData = new SignedHttpRequestCreationData(accessToken, httpRequestData, signingCredentials, creationPolicy);
            Assert.Equal(httpRequestData, creationData.HttpRequestData);
            Assert.Equal(accessToken, creationData.AccessToken);
            Assert.Equal(signingCredentials, creationData.SigningCredentials);
            Assert.Equal(creationPolicy, creationData.SignedHttpRequestCreationPolicy);
            Assert.NotNull(creationData.CallContext);

            creationData = new SignedHttpRequestCreationData(accessToken, httpRequestData, signingCredentials, creationPolicy, callContext);
            Assert.Equal(httpRequestData, creationData.HttpRequestData);
            Assert.Equal(accessToken, creationData.AccessToken);
            Assert.Equal(signingCredentials, creationData.SigningCredentials);
            Assert.Equal(creationPolicy, creationData.SignedHttpRequestCreationPolicy);
            Assert.Equal(callContext, creationData.CallContext);
        }

        [Fact]
        public void SignedHttpRequestValidationContext()
        {
            var httpRequestData = new HttpRequestData();
            var signedHttpRequest = SignedHttpRequestTestUtils.CreateDefaultSignedHttpRequestToken(SignedHttpRequestTestUtils.DefaultSignedHttpRequestPayload.ToString()).EncodedToken;
            var tokenValidationParameters = SignedHttpRequestTestUtils.DefaultTokenValidationParameters;
            var validationPolicy = new SignedHttpRequestValidationPolicy();
            var callContext = CallContext.Default;

            Assert.Throws<ArgumentNullException>("signedHttpRequest", () => new SignedHttpRequestValidationContext(null, httpRequestData, null, validationPolicy, callContext));
            Assert.Throws<ArgumentNullException>("signedHttpRequest", () => new SignedHttpRequestValidationContext(null, httpRequestData, null));
            Assert.Throws<ArgumentNullException>("signedHttpRequest", () => new SignedHttpRequestValidationContext(null, httpRequestData, null, (SignedHttpRequestValidationPolicy)null));
            Assert.Throws<ArgumentNullException>("signedHttpRequest", () => new SignedHttpRequestValidationContext(null, httpRequestData, null, (CallContext)null));
            Assert.Throws<ArgumentNullException>("signedHttpRequest", () => new SignedHttpRequestValidationContext(string.Empty, httpRequestData, null, validationPolicy, callContext));
            Assert.Throws<ArgumentNullException>("signedHttpRequest", () => new SignedHttpRequestValidationContext(string.Empty, httpRequestData, null));
            Assert.Throws<ArgumentNullException>("signedHttpRequest", () => new SignedHttpRequestValidationContext(string.Empty, httpRequestData, null, (SignedHttpRequestValidationPolicy)null));
            Assert.Throws<ArgumentNullException>("signedHttpRequest", () => new SignedHttpRequestValidationContext(string.Empty, httpRequestData, null, (CallContext)null));
            Assert.Throws<ArgumentNullException>("httpRequestData", () => new SignedHttpRequestValidationContext(signedHttpRequest, null, null, validationPolicy, callContext));
            Assert.Throws<ArgumentNullException>("httpRequestData", () => new SignedHttpRequestValidationContext(signedHttpRequest, null, null));
            Assert.Throws<ArgumentNullException>("httpRequestData", () => new SignedHttpRequestValidationContext(signedHttpRequest, null, null, (SignedHttpRequestValidationPolicy)null));
            Assert.Throws<ArgumentNullException>("httpRequestData", () => new SignedHttpRequestValidationContext(signedHttpRequest, null, null, (CallContext)null));
            Assert.Throws<ArgumentNullException>("accessTokenValidationParameters", () => new SignedHttpRequestValidationContext(signedHttpRequest, httpRequestData, null, validationPolicy, callContext));
            Assert.Throws<ArgumentNullException>("accessTokenValidationParameters", () => new SignedHttpRequestValidationContext(signedHttpRequest, httpRequestData, null));
            Assert.Throws<ArgumentNullException>("accessTokenValidationParameters", () => new SignedHttpRequestValidationContext(signedHttpRequest, httpRequestData, null, (SignedHttpRequestValidationPolicy)null));
            Assert.Throws<ArgumentNullException>("accessTokenValidationParameters", () => new SignedHttpRequestValidationContext(signedHttpRequest, httpRequestData, null, (CallContext)null));
            Assert.Throws<ArgumentNullException>("signedHttpRequestValidationPolicy", () => new SignedHttpRequestValidationContext(signedHttpRequest, httpRequestData, tokenValidationParameters, (SignedHttpRequestValidationPolicy)null));
            Assert.Throws<ArgumentNullException>("signedHttpRequestValidationPolicy", () => new SignedHttpRequestValidationContext(signedHttpRequest, httpRequestData, tokenValidationParameters, null, callContext));
            Assert.Throws<ArgumentNullException>("callContext", () => new SignedHttpRequestValidationContext(signedHttpRequest, httpRequestData, tokenValidationParameters, validationPolicy, null));

            // no exceptions
            var creationData = new SignedHttpRequestValidationContext(signedHttpRequest, httpRequestData, tokenValidationParameters);
            Assert.Equal(httpRequestData, creationData.HttpRequestData);
            Assert.Equal(signedHttpRequest, creationData.SignedHttpRequest);
            Assert.Equal(tokenValidationParameters, creationData.AccessTokenValidationParameters);
            Assert.NotNull(creationData.SignedHttpRequestValidationPolicy);
            Assert.NotNull(creationData.CallContext);

            creationData = new SignedHttpRequestValidationContext(signedHttpRequest, httpRequestData, tokenValidationParameters, callContext);
            Assert.Equal(httpRequestData, creationData.HttpRequestData);
            Assert.Equal(signedHttpRequest, creationData.SignedHttpRequest);
            Assert.Equal(tokenValidationParameters, creationData.AccessTokenValidationParameters);
            Assert.Equal(callContext, creationData.CallContext);
            Assert.NotNull(creationData.SignedHttpRequestValidationPolicy);
            

            creationData = new SignedHttpRequestValidationContext(signedHttpRequest, httpRequestData, tokenValidationParameters, validationPolicy);
            Assert.Equal(httpRequestData, creationData.HttpRequestData);
            Assert.Equal(signedHttpRequest, creationData.SignedHttpRequest);
            Assert.Equal(tokenValidationParameters, creationData.AccessTokenValidationParameters);
            Assert.Equal(validationPolicy, creationData.SignedHttpRequestValidationPolicy);
            Assert.NotNull(creationData.CallContext);

            creationData = new SignedHttpRequestValidationContext(signedHttpRequest, httpRequestData, tokenValidationParameters, validationPolicy, callContext);
            Assert.Equal(httpRequestData, creationData.HttpRequestData);
            Assert.Equal(signedHttpRequest, creationData.SignedHttpRequest);
            Assert.Equal(tokenValidationParameters, creationData.AccessTokenValidationParameters);
            Assert.Equal(validationPolicy, creationData.SignedHttpRequestValidationPolicy);
            Assert.Equal(callContext, creationData.CallContext);
        }
    }
}