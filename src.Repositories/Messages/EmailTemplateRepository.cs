﻿using Microsoft.EntityFrameworkCore;
using src.Core.Domains;
using src.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repositories.Messages
{
    public class EmailTemplateRepository: IEmailTemplateRepository
    {
        private readonly IDbContext _context;

        public EmailTemplateRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task<IList<EmailTemplate>> GetAllEmailTemplates()
        {
            var query = _context.EmailTemplates;

            return await query.ToListAsync();
        }

        public async Task<EmailTemplate> GetEmailTemplateById(Guid id)
        {
            var query = _context.EmailTemplates
                .Where(x => x.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<EmailTemplate> GetEmailTemplateByName(string name)
        {
            var query = _context.EmailTemplates
                .Where(x => x.Name == name);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Guid> InsertEmailTemplate(EmailTemplate template)
        {
            _context.EmailTemplates.Add(template);
            await _context.SaveChangesAsync();

            return template.Id;
        }

        public async Task UpdateEmailTemplate(EmailTemplate template)
        {
            _context.EmailTemplates.Update(template);
            await _context.SaveChangesAsync();
        }
    }
}
