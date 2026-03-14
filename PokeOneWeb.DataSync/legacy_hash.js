const MAX_CONCAT_LENGTH = 200;
const HASHING_ALGORITHM = Utilities.DigestAlgorithm.SHA_1;

const COL_A = 1;
const COL_B = 2;
const COL_C = 3;
const COL_D = 4;
const COL_E = 5;
const COL_F = 6;
const COL_G = 7;
const COL_H = 8;
const COL_I = 9;
const COL_J = 10;
const COL_K = 11;

function installedOnEdit(e) {
    var sheetName = e.range.getSheet().getName();
    var idColumns = [];

    switch (sheetName) {
        case "elemental_types":
        case "pvp_tiers":
        case "availabilities":
        case "abilities":
        case "spawn_types":
        case "bag_categories":
        case "move_damage_classes":
        case "currencies":
        case "natures":
        case "events":
        case "move_tutors":
            idColumns = [COL_C];
            break;
        case "regions":
        case "times_of_day":
        case "seasons":
            idColumns = [COL_D];
            break;
        case "moves":
            idColumns = [COL_E];
            break;
        case "items":
            idColumns = [COL_F];
            break;
        case "pokemon":
            idColumns = [COL_I];
            break;
        case "elemental_type_relations":
        case "season_times_of_day":
        case "tutor_moves":
        case "builds":
            idColumns = [COL_C, COL_D];
            break;
        case "move_learn_method_locations":
            idColumns = [COL_C, COL_F];
            break;
        case "evolutions":
            idColumns = [COL_E, COL_G];
            break;
        case "item_stat_boosts":
            idColumns = [COL_C, COL_J];
            break;
        case "hunting_configurations":
            idColumns = [COL_C, COL_D, COL_E];
            break;
        default:
            return;
    }

    UpdateHashes(e, idColumns);
}

function UpdateHashes(e, idColumns) {
    var id = e.source.getId();
    var sheetName = e.source.getActiveSheet().getSheetName();
    var sheet = SpreadsheetApp.openById(id).getSheetByName(sheetName);

    if (sheet.getRange("A1").getValues()[0][0] === "STOP") {
        return;
    }

    var rowCount = e.range.getNumRows();
    var minRow = e.range.getRow();

    // Ignore changes on first row of sheet
    if (minRow == 1) {
        minRow = 2;
        rowCount -= 1;
    }
    var maxRow = minRow + rowCount - 1;

    var colCount = e.range.getNumColumns();
    var minCol = e.range.getColumn();
    var maxCol = minCol + colCount - 1;

    var changedRangeName = "C" + minRow + ":" + maxRow; // Values are in column C onwards, i.e. "C3:8"
    var changedRange = sheet.getRange(changedRangeName);
    var changedRangeValues = changedRange.getValues();

    // Calculate and set hashes of changed rows
    var changedRowHashes = [];

    for (rowIndex = 0; rowIndex < rowCount; rowIndex++) {
        var values = changedRangeValues[rowIndex];
        var hash = GetRowHash(values);

        changedRowHashes.push([hash]); // Push hash as array as hashes will be inserted as a column instead of a row.
    }

    var changedHashRangeName = "A" + minRow + ":A" + maxRow;
    sheet.getRange(changedHashRangeName).setValues(changedRowHashes);

    // Calculate and set new sheet hash
    var hashRangeName = "A2:A";
    var hashRange = sheet.getRange(hashRangeName);
    var allHashes = hashRange.getValues();
    var sheetHash = GetSheetHash(allHashes);
    sheet.getRange("A1").setValue(sheetHash);

    // Determine, if ID columns have changed. If yes, update ID Hashes
    var hasIdChanges = false;
    for (i = 0; i < idColumns.length; i++) {
        // Check if any ID column lies within the changed range
        if (idColumns[i] >= minCol && idColumns[i] <= maxCol) {
            hasIdChanges = true;
            break;
        }
    }

    if (hasIdChanges) {
        // Get all id columns (including columns outside changed range) for rows within changed range
        var rangeListNotation = getRangeListNotation(idColumns, minRow, maxRow);
        var idValueRanges = sheet.getRangeList(rangeListNotation).getRanges();
        var idValues = []
        for (i = 0; i < idValueRanges.length; i++) {
            idValues.push(idValueRanges[i].getValues().map(x => x[0])); // Get first column of each range
        }

        // Re-calculate id hashes for changed range
        var changedIdHashes = [];

        for (rowIndex = 0; rowIndex < rowCount; rowIndex++) {
            var values = [];
            for (col = 0; col < idValues.length; col++) {
                values.push(idValues[col][rowIndex]);
            }

            var hash = GetRowHash(values);

            changedIdHashes.push([hash]); // Push hash as array as hashes will be inserted as a column instead of a row.
        }

        // Write changed id hashes
        var changedIdHashRangeName = "B" + minRow + ":B" + maxRow;
        sheet.getRange(changedIdHashRangeName).setValues(changedIdHashes);
    }
}

function GetRowHash(input) {
    var txtInput = Array.isArray(input) ? input.join('') : input;
    var rawHash = Utilities.computeDigest(HASHING_ALGORITHM, encodeNonAsciiCharacters(txtInput));
    return GetTextHashFromRawHash(rawHash);
}

function GetSheetHash(input) {
    if (!Array.isArray(input)) {
        var rawHash = Utilities.computeDigest(HASHING_ALGORITHM, input);
        return GetTextHashFromRawHash(rawHash);
    }

    var clusteredHashes = [];
    for (i = 0; i < input.length; i += MAX_CONCAT_LENGTH) {
        var slice = input.slice(i, i + MAX_CONCAT_LENGTH);
        var rawHash = Utilities.computeDigest(HASHING_ALGORITHM, slice.join(''));
        clusteredHashes.push(GetTextHashFromRawHash(rawHash));
    }

    var rawHash = Utilities.computeDigest(HASHING_ALGORITHM, clusteredHashes.join(''));

    return GetTextHashFromRawHash(rawHash);
}

function GetTextHashFromRawHash(rawHash) {
    var txtHash = '';
    for (j = 0; j < rawHash.length; j++) {
        var hashVal = rawHash[j];
        if (hashVal < 0)
            hashVal += 256;
        if (hashVal.toString(16).length == 1)
            txtHash += "0";
        txtHash += hashVal.toString(16);
    }

    return txtHash;
}

function getRangeListNotation(columnIndexes, minRow, maxRow) {
    var rangeList = [];
    for (var i = 0; i < columnIndexes.length; i++) {
        var columnLetters = getColumnLetters(columnIndexes[i] - 1); // -1 as indexes are 1-indicated
        rangeList.push(`${columnLetters}${minRow}:${columnLetters}${maxRow}`);
    }

    return rangeList;
}

function getColumnLetters(i) {
    const m = i % 26;
    const c = String.fromCharCode(65 + m);
    const r = i - m;
    return r > 0
        ? `${getColumnLetter((r - 1) / 26)}${c}`
        : `${c}`
}

function encodeNonAsciiCharacters(value) {
    let out = ""
    for (let i = 0; i < value.length; i++) {
        const ch = value.charAt(i);
        let chn = ch.charCodeAt(0);
        if (chn <= 127) out += ch;
        else {
            let hex = chn.toString(16);
            if (hex.length < 4)
                hex = "000".substring(hex.length - 1) + hex;
            out += "\\u" + hex;
        }
    }
    return out;
}