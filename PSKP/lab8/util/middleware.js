const { defineAbilityFor, defineRulesFor } = require('./abilitites')


exports.abilityCheckMiddleware = (action, subject) => {
    return function abilityCheck(req, res, next) {
        const ability = defineAbilityFor(req.user)
        if(ability.can(action, subject)) next()
        else next(new Error(`you cant ${action} ${subject}`))
    }
}