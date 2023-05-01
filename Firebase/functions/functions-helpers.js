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

const admin = require("firebase-admin");
const crypto = require("crypto");

function sha256(string) {
	return crypto.createHash('sha256').update(string).digest('hex');
}

async function createScore(name, score, hash, firestore) {

	// If someone try to cheat, do not add entry and return here
	const key = "b4fs8Bg6r4fbs5fRGwb5gHd7hqkz6g"; // fake key haha, i've changed it after noticing i've commited it
	const computedHash = sha256(name + score + key);
	if (computedHash !== hash) {
		console.log("Someone tried to cheat! (name=", name, ", score=", score, ", hash=", hash, ")");
		return [];
	}

	// Add player name and score in the leaderboard
	const result = await firestore.collection("scores").doc().create({
		name: name,
		score: score,
	});

	const LEADERBOARD_SIZE = 10;
	const NB_BEST_SCORES = 5;
	const NB_ABOVE = 2;
	const NB_BELOW = 2;

	// Fetch all scores
	const scores = await firestore.collection("scores").orderBy("score", "desc").get();

	const res = [];

	let rank = 1;
	let playerRank = 0;

	// Get current player rank
	for (const doc of scores.docs) {

		const currentPlayerName = `${doc.get("name")}`;
		const currentScore = doc.get("score");

		if (name === currentPlayerName && score === currentScore) {
		    playerRank = rank;
		}

		rank++;
	}

	// Fill result object (either with 10 top scores, or 5 top scores + score around the player)
	if (playerRank <= LEADERBOARD_SIZE) {
		scores.docs.slice(0, LEADERBOARD_SIZE).forEach((doc, index) => {
			res.push({
				name: `${doc.get("name")}`,
				score: doc.get("score"),
				rank: index + 1,
			});
		})
	} else {
		scores.docs.slice(0, NB_BEST_SCORES).forEach((doc, index) => {
			res.push({
				name: `${doc.get("name")}`,
				score: doc.get("score"),
				rank: index + 1,
			});
		})
		scores.docs.slice(playerRank - NB_ABOVE - 1, playerRank + NB_BELOW).forEach((doc, index) => {
			res.push({
				name: `${doc.get("name")}`,
				score: doc.get("score"),
				rank: playerRank - NB_ABOVE + index + 1,
			});
		})
	}

	return res;
}

async function getScores(firestore) {
	const scores = await firestore.collection("scores").orderBy("score", "desc").get();
	return scores.docs.slice(0, 10).map((doc, index) => {
		return {
			name: `${doc.get("name")}`,
			score: doc.get("score"),
			rank: index + 1,
		};
	});
}


module.exports = {createScore, getScores};