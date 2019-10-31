 curl -v -X POST https://api.line.me/v2/bot/richmenu \
  -H 'Authorization: Bearer yVYd4NMAG3SXN24DKBriCLQS2MxUjuDjKwqWV/voUbWiWvnGNxV0nyUEW+TyHcCZrxCgY+3wNsXPbXvL7JCHhJ55VTFpkdqLckv+Pa9WdNI8SAeoSK3TjbSWqPb27dkDGGjhnzUWSOoAnY5Zvon6igdB04t89/1O/w1cDnyilFU=' \
  -H 'Content-Type:application/json' \
  -d \
  '{
    "size":{
        "width":2500,
        "height":843
    },
    "selected":false,
    "name":"Controller",
    "chatBarText":"Controller",
    "areas":[
        {
          "bounds":{
              "x":0,
              "y":0,
              "width":1132,
              "height":831
          },
          "action":{
              "type":"message",
              "text":"Testing"
          }
        },
        {
          "bounds":{
              "x":1132,
              "y":11,
              "width":1364,
              "height":843
          },
          "action":{
              "type":"message",
              "text":"news"
          }
        }
    ]
  }'

cmd /k