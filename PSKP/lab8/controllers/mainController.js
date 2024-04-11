const express = require('express')
const ApiController = require('../Services/mainService')
const { abilityCheckMiddleware } = require('../util/middleware')

let router = express.Router()

router.get('/api/ability',[abilityCheckMiddleware('read', 'Abilities')], ApiController.getUserAbilities)
router.get('/api/user', [abilityCheckMiddleware('read', 'Users')], ApiController.getUsersList)
router.get('/api/user/:id', [abilityCheckMiddleware('read', 'Users')], ApiController.getUserInfo)
router.get('/api/repos', [abilityCheckMiddleware('read', 'Repos')], ApiController.getReposList)
router.get('/api/repos/:id', [abilityCheckMiddleware('read', 'Repos')], ApiController.getRepoInfo)
router.get('/api/repos/:id/commits', [abilityCheckMiddleware('read', 'Commits')], ApiController.getReposCommits)
router.get('/api/repos/:id/commits/:commitId', [abilityCheckMiddleware('read', 'Commits')],  ApiController.getCommitInfo)

router.post('/api/repos', [abilityCheckMiddleware('create', 'Repos')], ApiController.addRepo)
router.post('/api/repos/:id/commits', [abilityCheckMiddleware('create', 'Commits')], ApiController.addCommit)

router.put('/api/repos/:id', [abilityCheckMiddleware('update', 'Repos')], ApiController.editRepo)
router.put('/api/repos/:id/commits/:commitId', [abilityCheckMiddleware('update', 'Commits')],  ApiController.editCommit)

router.delete('/api/repos/:id', [abilityCheckMiddleware('delete', 'Repos')], ApiController.deleteRepo)
router.delete('/api/repos/:id/commits/:commitId', [abilityCheckMiddleware('delete', 'Commits')], ApiController.deleteCommit)

module.exports = router