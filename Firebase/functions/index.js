/**
 * Copyright 2022 Google LLC. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
"use strict";

const cors = require("cors")({
  // This is for testing. Don't do it in a real app.
  // See the documentation for more details:
  // https://firebase.google.com/docs/functions/beta/http-events
  origin: true,
});

const functions = require("firebase-functions");
const admin = require("firebase-admin");
const helpers = require("./functions-helpers.js");
const utils = require("./utils.js");
admin.initializeApp();

// Adds 10 random scores.
exports.addScores = functions.https.onRequest(async (req, res) => {
  cors(req, res, () => {
    const scores = [];
    for (let i = 0; i < 10; i++) {
      scores.push({
        name: utils.newUserID(),
        score: utils.randomScore(),
      });
    }
    // This is done synchronously to avoid lock contention when using
    // transactions.
    Promise.all(
        scores.map(async (score) => {
          await helpers.createScore(
              score.name,
              score.score,
              admin.firestore(),
          );
        }),
    ).then((results) => {
      res.json({
        result: "Added scores",
        writes: results,
      });
      res.end();
    });
  });
});

exports.addScore = functions.https.onRequest(async (req, res) => {
  cors(req, res, () => {
    const name = req.body.name;
    const score = req.body.score;
    const hash = req.body.hash;
    helpers.createScore(name, score, hash, admin.firestore()).then((result) => {
      res.json({Items: result});
    });
  });
});

exports.getScores = functions.https.onRequest(async (req, res) => {
    cors(req, res, () => {
        helpers.getScores(admin.firestore()).then((result) => {
            res.json({Items: result});
        })
    });
})
