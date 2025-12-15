window.nurseNotes = window.nurseNotes || {
  _key: "nurseNotes.noteLockedPatientIds",

  getNoteLockedPatientIds: function () {
    try {
      return window.localStorage.getItem(this._key) || "";
    } catch {
      return "";
    }
  },

  _getSet: function () {
    const raw = this.getNoteLockedPatientIds();
    const set = new Set();
    if (!raw) return set;
    raw.split(",").forEach((s) => {
      const v = parseInt((s || "").trim(), 10);
      if (!isNaN(v) && v > 0) set.add(v);
    });
    return set;
  },

  _saveSet: function (set) {
    try {
      const arr = Array.from(set.values()).sort((a, b) => a - b);
      window.localStorage.setItem(this._key, arr.join(","));
    } catch {
      // ignore
    }
  },

  lockPatientId: function (patientId) {
    const id = parseInt(patientId, 10);
    if (isNaN(id) || id <= 0) return;
    const set = this._getSet();
    set.add(id);
    this._saveSet(set);
  },

  unlockPatientId: function (patientId) {
    const id = parseInt(patientId, 10);
    if (isNaN(id) || id <= 0) return;
    const set = this._getSet();
    set.delete(id);
    this._saveSet(set);
  },

  clearAll: function () {
    try {
      window.localStorage.removeItem(this._key);
    } catch {
      // ignore
    }
  },
};
